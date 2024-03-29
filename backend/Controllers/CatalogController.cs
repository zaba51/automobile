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

    [HttpPost]
    public async Task<ActionResult<IEnumerable<CatalogItem>>> GetAvailableItems(CatalogRequest request)
    {
        var items = await _catalogRepository
            .GetMatchingCatalogItemsAsync(request.BeginTime, request.Duration, request.LocationId);

        return Ok(items);
    }

    [HttpGet]
    [Route("{itemId}")]
    public async Task<ActionResult<IEnumerable<CatalogItem>>> GetItemById(int itemId)
    {
        var item = await _catalogRepository
            .GetItemByQuery(item => item.Id == itemId);

        if (item != null)
        {
            return Ok(item);
        }
        return NotFound();
    }

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