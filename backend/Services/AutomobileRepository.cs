using System.Linq.Expressions;
using automobile.Helpers;
using backend.DbContexts;
using backend.Entities;
using backend.Models;
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

        public async Task<Model?> GetModelByQuery(Expression<Func<Model, bool>>  predicate) {
            return await _context.Models.Where(predicate).FirstOrDefaultAsync();
        }
        
        public void AddCatalogItem(CatalogItem catalogItem)
        {
            _context.CatalogItems.Add(catalogItem);
        }

        public void AddModel(Model model)
        {
            _context.Models.Add(model);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() >= 0;
        }

        public async Task<IEnumerable<Reservation>> GetReservationsForUserAsync(int userId)
        {
            return await _context.Reservations
                .Include(r => r.DriversDetails)
                .Include(r => r.CatalogItem)
                    .ThenInclude(item => item.Model)
                .Where(r => r.UserId == userId)
                .OrderBy(r => r.BeginTime)
                .ToListAsync();
        }

        public async void AddReservation(int userId, Reservation reservation)
        {
            _context.DriversDetails.Add(reservation.DriversDetails);

            _context.Reservations.Add(reservation);
        }
    }
}
