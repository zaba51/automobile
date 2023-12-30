using System.Linq.Expressions;
using backend.Entities;
using backend.Models;

namespace backend.Services {
    public interface IAutomobileRepository
    {
        Task<IEnumerable<CatalogItem>> GetCatalogItemsAsync();

        Task<IEnumerable<CatalogItem>> GetMatchingCatalogItemsAsync(DateTime BeginTime, int Duration, string Location);

        void AddCatalogItem(CatalogItem catalogItem);
        void AddModel(Model model);

        Task<bool> SaveChangesAsync();

        Task<IEnumerable<Reservation>> GetReservationsForUserAsync(int userId);
    
        void AddReservation(int userId, Reservation reservation);

        Task<Model?> GetModelByQuery(Expression<Func<Model, bool>>  predicate);
    }
}
