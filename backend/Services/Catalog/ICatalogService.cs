using automobile.Models;

namespace backend.Services {
    public interface ICatalogService {
        public Task AddCatalogItemAsync(AddCatalogItemDTO item, IFormFile file);
    }
}