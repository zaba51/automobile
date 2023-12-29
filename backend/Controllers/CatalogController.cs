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
}