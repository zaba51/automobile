using System.Data;
using System.Linq.Expressions;
using automobile.Helpers;
using backend.DbContexts;
using backend.Entities;
using backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace backend.Services {
    public class ReservationRepository: AutomobileRepository, IReservationRepository
    { 
        public ReservationRepository(AutomobileContext context)
            :base(context)
        {}

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
    }
}