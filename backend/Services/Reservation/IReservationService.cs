using backend.Entities;
using backend.Models;

namespace backend.Services {
    public interface IReservationService {
        public Task<(Reservation NewReservation, CatalogItem CatalogItem)> AddReservationAsync(int userId, AddReservationDTO reservation);
    }
}