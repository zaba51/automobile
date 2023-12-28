using backend.Entities;
using backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("[controller]")]
public class CatalogController : ControllerBase
{
    private readonly IAutomobileRepository _automobileRepository;
    public CatalogController(IAutomobileRepository automobileRepository)
    {
        _automobileRepository = automobileRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CatalogItem>>> GetAvailableItems() {
        var items = await _automobileRepository.GetCatalogItemsAsync();

        return Ok(items);
    }
}