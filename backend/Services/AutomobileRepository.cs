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
                .Include(item => item.Supplier)
                .Include(item => item.Location)
                .ToListAsync();
        }

        public async Task<IEnumerable<CatalogItem>> GetMatchingCatalogItemsAsync(DateTime beginTime, int duration, int locationId)
        {
            // var unavailable = await _context.Reservations
            //     .Where(r => TimeHelper.IsOverlapping(beginTime, beginTime.AddHours(duration), r.BeginTime, r.EndTime))
            //     .Select(r => r.CatalogItem)
            //     .ToListAsync();\
            var reservations = await _context.Reservations.ToListAsync();

            var unavailable = await _context.Reservations
                .Where(r => (r.BeginTime >= beginTime && r.BeginTime < beginTime.AddHours(duration)) ||
                            (r.EndTime > beginTime && r.EndTime <= beginTime.AddHours(duration)) ||
                            (beginTime >= r.BeginTime && beginTime.AddHours(duration) <= r.EndTime)
                        )
                .Select(r => r.CatalogItem)
                // .Include(c => c.Location)
                // .Include(c => c.Model)
                // .Include(c => c.Location)
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
                 .Include(r => r.CatalogItem.Supplier)
                 .Include(r => r.CatalogItem.Location)
                .Where(r => r.UserId == userId)
                .OrderBy(r => r.BeginTime)
                .ToListAsync();
        }

        public async void AddReservation(int userId, Reservation reservation)
        {
            _context.DriversDetails.Add(reservation.DriversDetails);

            _context.Reservations.Add(reservation);
        }

        public async Task<bool> UserExistsAsync(int userId)
        {
            return await _context.Users.AnyAsync(u => u.Id == userId);
        }

        public async Task<Reservation?> GetSingleReservationForUserAsync(int userId, int reservationId)
        {
            return await _context.Reservations
                .Where(r => r.UserId == userId && r.Id == reservationId).FirstOrDefaultAsync();
        }

        public async Task DeleteReservation(Reservation reservation)
        {
            var driversDetails = await _context.DriversDetails.Where(d => d.Id == reservation.DriversDetailsId).FirstOrDefaultAsync();
            if (driversDetails != null) {
                _context.DriversDetails.Remove(driversDetails);
            } 
            _context.Reservations.Remove(reservation);
        }

        public async Task<User?> GetUserByCredentialsAsync(string? email, string? password)
        {
            return await _context.Users.Where(u => u.Email == email && u.Password == password).FirstOrDefaultAsync();
        }

        public async Task<User?> GetSingleUserAsync(int userID)
        {
            return await _context.Users.Where(u => u.Id == userID).FirstOrDefaultAsync();
        }

        public async Task<User?> GetUserByUsernameAsync(string email) {
            return await _context.Users.Where(u => u.Email == email).FirstOrDefaultAsync();
        }

        public void AddUser(User user)
        {
            _context.Users.Add(user);
        }

        public async Task<IEnumerable<Location>> GetLocations()
        {
            return await _context.Locations.ToListAsync();
        }

        public async Task<Supplier?> GetSupplierByQuery(Expression<Func<Supplier, bool>>  predicate) {
            return await _context.Suppliers
                .Where(predicate)
                .Include(s => s.Locations)
                .FirstOrDefaultAsync();
        }
        
    }
}
