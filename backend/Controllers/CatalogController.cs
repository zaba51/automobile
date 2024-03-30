using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Serialization;
using automobile.Models;
using backend.Entities;
using backend.models.DTO;
using backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace backend.Controllers;

[ApiController]
[Route("api/catalog")]
// [Authorize]
public class CatalogController : ControllerBase
{
    private readonly ICatalogRepository _catalogRepository;
    private readonly IWebHostEnvironment _hostEnvironment;

    public CatalogController(
        ICatalogRepository catalogRepository,
        IWebHostEnvironment hostEnvironment    
    )
    {
        _catalogRepository = catalogRepository;
        _hostEnvironment = hostEnvironment;
    }

    /// <summary>
    /// GetAvailableItems
    /// </summary>
    /// <param name="request">CatalogRequest specifying item parameters</param>
    /// <returns>CatalogItems</returns>
    /// <response code="200">Available items matching request</response>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpPost]
    public async Task<ActionResult<IEnumerable<CatalogItem>>> GetAvailableItems(CatalogRequest request)
    {
        var items = await _catalogRepository
            .GetMatchingCatalogItemsAsync(request.BeginTime, request.Duration, request.LocationId);

        return Ok(items);
    }

    /// <summary>
    /// GetItemById
    /// </summary>
    /// <param name="itemId">Id of the requested item</param>
    /// <returns>CatalogItem</returns>
    /// <response code="200">Requested item details</response>
    /// <response code="404">Item doesn't exist</response>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet]
    [Route("{itemId}")]
    public async Task<ActionResult<CatalogItem>> GetItemById(int itemId)
    {
        var item = await _catalogRepository
            .GetItemByQuery(item => item.Id == itemId);

        if (item != null)
        {
            return Ok(item);
        }
        return NotFound();
    }

    /// <summary>
    /// Retrieves a list of available locations.
    /// </summary>
    /// <returns>
    /// Returns an ActionResult containing the list of locations if available, 
    /// else returns a NotFound result.
    /// </returns>
    /// <response code="200">Locations</response>
    /// <response code="404">Locations not found</response>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("locations")]
    public async Task<ActionResult<IEnumerable<Location>>> GetLocations()
    {
        var item = await _catalogRepository.GetLocations();

        if (item != null)
        {
            return Ok(item);
        }
        return NotFound();
    }

    /// <summary>
    /// AddCatalogItem
    /// </summary>
    /// <param name="addItemDto">AddItemDto to add Catalog item</param>
    /// <returns>ActionResult</returns>
    /// <response code="200">Logged in succesfully</response>
    /// <response code="400">Error during deserialization</response>
    /// <response code="401">Unauthenticated access</response>
    /// <response code="403">Access without permission to resource</response>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [HttpPost("add")]
    [Authorize(Policy = "OnlySupplier")]
    public async Task<ActionResult> AddCatalogItem([FromForm] AddItemDto addItemDto)
    {
        try
        {
            var newItemJson = addItemDto.NewItem;
            var item = JsonConvert.DeserializeObject<AddCatalogItemDTO>(newItemJson);

            var requestingSupplierId = User.Claims.FirstOrDefault(c => c.Type == "supplierId")?.Value;

            if (Convert.ToInt32(requestingSupplierId) != item.SupplierId)
            {
                return Forbid();
            }

            var existingModel = await _catalogRepository.GetModelByQuery((model) => (
                model.Company == item.Model.Company &&
                model.Name == item.Model.Name &&
                model.Power == item.Model.Power &&
                model.Gear == item.Model.Gear &&
                model.DoorCount == item.Model.DoorCount &&
                model.SeatCount == item.Model.SeatCount &&
                model.Engine == item.Model.Engine &&
                model.Color == item.Model.Color
            ));
            CatalogItem newItem;

            if (existingModel != null)
            {
                newItem = new CatalogItem()
                {
                    ModelId = existingModel.Id,
                    Price = item.Price,
                    SupplierId = item.SupplierId,
                    LocationId = item.LocationId
                };
            }
            else
            {
                var file = addItemDto.File;
                string relativeFilePath = "";

                if (file != null && file.Length > 0)
                {
                    var uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "assets", "cars");

                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    relativeFilePath = Path.Combine("assets", "cars", uniqueFileName);
                }

                item.Model.ImageUrl = relativeFilePath;

                _catalogRepository.AddModel(item.Model);

                await _catalogRepository.SaveChangesAsync();

                var model = await _catalogRepository.GetModelByQuery((model) => model == item.Model);

                newItem = new CatalogItem()
                {
                    ModelId = model.Id,
                    Price = item.Price,
                    SupplierId = item.SupplierId,
                    LocationId = item.LocationId
                };
            }

            _catalogRepository.AddCatalogItem(newItem);

            await _catalogRepository.SaveChangesAsync();

            return Ok();
        }
        catch (Exception e)
        { 
            return BadRequest();
        }
    }
    /// <summary>
    /// GetSupplierInfo
    /// </summary>
    /// <param name="supplierId">Supplier's Id</param>
    /// <returns>SupplierInfo</returns>
    /// <response code="200">Information about the supplier</response>
    /// <response code="404">Supplier does not exist</response>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<SupplierInfo>>> GetSupplierInfo([FromQuery(Name = "supplierId")] int supplierId)
    {
        var supplier = await _catalogRepository.GetSupplierByQuery(s => s.Id == supplierId);

        if (supplier == null)
        {
            return NotFound();
        }

        var items = await _catalogRepository.GetItemsByQuery(item => item.Supplier.Id == supplierId);

        var supplierInfo = new SupplierInfo()
        {
            CatalogItems = items,
            Supplier = supplier
        };
        return Ok(supplierInfo);
    }

}