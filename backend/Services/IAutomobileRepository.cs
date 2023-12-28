using backend.Entities;

namespace backend.Services {
    public interface IAutomobileRepository
    {
        Task<IEnumerable<CatalogItem>> GetCatalogItemsAsync();
    }
}
