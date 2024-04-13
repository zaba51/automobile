using System.Data;
using System.Linq.Expressions;
using backend.Entities;
using backend.Models;
using Microsoft.EntityFrameworkCore.Storage;

namespace backend.Services {
    public interface ICatalogRepository:IAutomobileRepository
    {
        Task<IEnumerable<CatalogItem>> GetCatalogItemsAsync();

        Task<IEnumerable<CatalogItem>> GetMatchingCatalogItemsAsync(DateTime BeginTime, int Duration, int locationId);

        void AddCatalogItem(CatalogItem catalogItem);
        
        void AddModel(Model model);
        Task<Model?> GetModelByQuery(Expression<Func<Model, bool>>  predicate);
        Task<CatalogItem?> GetItemByQuery(Expression<Func<CatalogItem, bool>>  predicate);
        Task<IEnumerable<CatalogItem>> GetItemsByQuery(Expression<Func<CatalogItem, bool>>  predicate);
        Task<Supplier?> GetSupplierByQuery(Expression<Func<Supplier, bool>>  predicate);

        Task<CarCompany?> GetCarCompanyByQuery(Expression<Func<CarCompany, bool>>  predicate);

        Task<IEnumerable<AdditionalService>> GetAdditionalServices();
    }
}