using System.Data;
using System.Linq.Expressions;
using backend.Entities;
using backend.Models;
using Microsoft.EntityFrameworkCore.Storage;

namespace backend.Services {
    public interface IReservationRepository: IAutomobileRepository
    {
        Task<IEnumerable<Reservation>> GetReservationsForUserAsync(int userId);
        Task<Reservation?> GetSingleReservationForUserAsync(int userId, int reservationId);
        void AddReservation(int userId, Reservation reservation);
        Task DeleteReservation(Reservation reservation);
    }
}
