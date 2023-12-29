using backend.DbContexts;
using backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace backend.Services {
    public class AutomobileRepository: IAutomobileRepository
    {
        // private List<CatalogItem> catalogItems;
        private readonly AutomobileContext _context;

        public AutomobileRepository(AutomobileContext context)
        {
            // catalogItems = new List<CatalogItem>{};
            // catalogItems.Add(new CatalogItem() {
            //     Id = 1,
            //     Model = null,
            //     Price = 100,
            //     Company = "My company"
            // });
            _context = context ?? throw new ArgumentException(nameof(context));
        }

        public async Task<IEnumerable<CatalogItem>> GetCatalogItemsAsync() {
            return await _context.CatalogItems.ToListAsync();
        }
    }
}
