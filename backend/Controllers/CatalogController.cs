using automobile.Models;
using backend.Entities;
using backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/catalog")]
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
            .GetMatchingCatalogItemsAsync(request.BeginTime, request.Duration, request.Location);

        return Ok(items);
    }

    [HttpPost("add")]
    public async Task<ActionResult> AddCatalogItem(AddCatalogItemDTO item)
    {
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

                Supplier = item.Supplier
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

                Supplier = item.Supplier
            };
        }

        _automobileRepository.AddCatalogItem(newItem);

        await _automobileRepository.SaveChangesAsync();

        return Ok();
    }
}