using System.Data;
using System.Linq.Expressions;
using automobile.Helpers;
using backend.DbContexts;
using backend.Entities;
using backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace backend.Services {
    public class CatalogRepository: AutomobileRepository, ICatalogRepository
    { 
        public CatalogRepository(AutomobileContext context)
            :base(context)
        {}

        public async Task<IEnumerable<CatalogItem>> GetCatalogItemsAsync() {
            return await _context.CatalogItems
                .Include(item => item.Model)
                .Include(item => item.Supplier)
                .Include(item => item.Location)
                .ToListAsync();
        }

        public async Task<IEnumerable<CatalogItem>> GetMatchingCatalogItemsAsync(DateTime beginTime, int duration, int locationId)
        {
            var reservations = await _context.Reservations.ToListAsync();

            var unavailable = await _context.Reservations
                .Where(r => (r.BeginTime >= beginTime && r.BeginTime < beginTime.AddHours(duration)) ||
                            (r.EndTime > beginTime && r.EndTime <= beginTime.AddHours(duration)) ||
                            (beginTime >= r.BeginTime && beginTime.AddHours(duration) <= r.EndTime)
                        )
                .Select(r => r.CatalogItem)
                .ToListAsync();

            var all = await GetCatalogItemsAsync();

            return all.Where(item => !unavailable.Contains(item) && item.Location.Id == locationId);            
        }

        public async Task<Model?> GetModelByQuery(Expression<Func<Model, bool>>  predicate) {
            return await _context.Models.Where(predicate).FirstOrDefaultAsync();
        }

        public async Task<CatalogItem?> GetItemByQuery(Expression<Func<CatalogItem, bool>>  predicate) {
            return await _context.CatalogItems
                .Where(predicate)
                .Include(item => item.Model)
                .Include(item => item.Supplier)
                .Include(item => item.Location)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<CatalogItem>> GetItemsByQuery(Expression<Func<CatalogItem, bool>>  predicate) {
            return await _context.CatalogItems
                .Where(predicate)
                .Include(item => item.Model)
                .Include(item => item.Supplier)
                .Include(item => item.Location)
                .ToListAsync();
        }
        
        public void AddCatalogItem(CatalogItem catalogItem)
        {
            _context.CatalogItems.Add(catalogItem);
        }

        public void AddModel(Model model)
        {
            _context.Models.Add(model);
        }

        public async Task<Supplier?> GetSupplierByQuery(Expression<Func<Supplier, bool>>  predicate) {
            return await _context.Suppliers
                .Where(predicate)
                .Include(s => s.Locations)
                .FirstOrDefaultAsync();
        }
    }

}