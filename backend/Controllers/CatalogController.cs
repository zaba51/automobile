using System.Security.Claims;
using automobile.Models;
using backend.Entities;
using backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/catalog")]
// [Authorize]
public class CatalogController : ControllerBase
{
    private readonly IAutomobileRepository _automobileRepository;
    public CatalogController(IAutomobileRepository automobileRepository)
    {
        _automobileRepository = automobileRepository;
    }

    [HttpPost]
    public async Task<ActionResult<IEnumerable<CatalogItem>>> GetAvailableItems(CatalogRequest request) {
        var items = await _automobileRepository
            .GetMatchingCatalogItemsAsync(request.BeginTime, request.Duration, request.LocationId);

        return Ok(items);
    }

    [HttpGet]
    [Route("{itemId}")]
    public async Task<ActionResult<IEnumerable<CatalogItem>>> GetItemById(int itemId) {
        var item = await _automobileRepository
            .GetItemByQuery(item => item.Id == itemId);

        if (item != null) {
            return Ok(item);
        }
        return NotFound();
    }

    [HttpGet("locations")]
    public async Task<ActionResult<IEnumerable<Location>>> GetLocations() {
        var item = await _automobileRepository.GetLocations();

        if (item != null) {
            return Ok(item);
        }
        return NotFound();
    }

    [HttpPost("add")]
    [Authorize(Policy = "OnlySupplier")]
    public async Task<ActionResult> AddCatalogItem(AddCatalogItemDTO item)
    {

        // var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

        // if(userRole != "supplier") {
        //     return Forbid();
        // }

        var existingModel = await _automobileRepository.GetModelByQuery((model) => (
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

        if (existingModel != null) {
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
            _automobileRepository.AddModel(item.Model);
    
            await _automobileRepository.SaveChangesAsync();

            var model = await _automobileRepository.GetModelByQuery((model) => model == item.Model);

            newItem = new CatalogItem()
            {
                ModelId = model.Id,
                Price = item.Price,
                SupplierId = item.SupplierId,
                LocationId = item.LocationId
            };
        }

        _automobileRepository.AddCatalogItem(newItem);

        await _automobileRepository.SaveChangesAsync();

        return Ok();
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<SupplierInfo>>> GetSupplierInfo([FromQuery(Name="supplierId")] int supplierId)
    {
        var supplier = await _automobileRepository.GetSupplierByQuery(s => s.Id == supplierId);

        if (supplier == null) {
            return NotFound();
        }

        var items = await _automobileRepository.GetItemsByQuery(item => item.Supplier.Id == supplierId);

        var supplierInfo = new SupplierInfo() {
            CatalogItems = items,
            Supplier = supplier
        };
        return Ok(supplierInfo);
    }

}