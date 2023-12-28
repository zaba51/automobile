using backend.Entities;

namespace backend.Services {
    public class AutomobileRepository: IAutomobileRepository
    {
        private List<CatalogItem> catalogItems;

        public AutomobileRepository()
        {
            catalogItems = new List<CatalogItem>{};
            catalogItems.Add(new CatalogItem() {
                Id = 1,
                Model = null,
                Price = 100,
                Company = "My company"
            });
        }

        public async Task<IEnumerable<CatalogItem>> GetCatalogItemsAsync() {
            return catalogItems;
        }
    }
}
