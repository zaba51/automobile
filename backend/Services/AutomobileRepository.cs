using automobile.Helpers;
using backend.DbContexts;
using backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace backend.Services {
    public class AutomobileRepository: IAutomobileRepository
    {
        private readonly AutomobileContext _context;

        public AutomobileRepository(AutomobileContext context)
        {
            _context = context ?? throw new ArgumentException(nameof(context));
        }

        public async Task<IEnumerable<CatalogItem>> GetCatalogItemsAsync() {
            return await _context.CatalogItems
                .Include(item => item.Model)
                .ToListAsync();
        }

        public async Task<IEnumerable<CatalogItem>> GetMatchingCatalogItemsAsync(DateTime beginTime, int duration, string location)
        {
            // var unavailable = await _context.Reservations
            //     .Where(r => TimeHelper.IsOverlapping(beginTime, beginTime.AddHours(duration), r.BeginTime, r.EndTime))
            //     .Select(r => r.CatalogItem)
            //     .ToListAsync();\
            var unavailable = await _context.Reservations
                .Where(r => (r.BeginTime >= beginTime && r.BeginTime < beginTime.AddHours(duration)) ||
                            (r.EndTime > beginTime && r.EndTime <= beginTime.AddHours(duration)) ||
                            (beginTime >= r.BeginTime && beginTime.AddHours(duration) <= r.EndTime))
                .Select(r => r.CatalogItem)
                .ToListAsync();

            var all = await GetCatalogItemsAsync();

            return all.Where(item => !unavailable.Contains(item));            
        }
    }
}
