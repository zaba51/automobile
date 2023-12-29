using backend.Entities;

namespace backend.Services {
    public interface IAutomobileRepository
    {
        Task<IEnumerable<CatalogItem>> GetCatalogItemsAsync();

        Task<IEnumerable<CatalogItem>> GetMatchingCatalogItemsAsync(DateTime BeginTime, int Duration, string Location);
    }
}
