using System.Security.Claims;
using backend.Entities;
using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Authorize]
[Route("api/users/{userId}/reservations")]
public class ReservationController : ControllerBase
{
    
    private readonly IAutomobileRepository _automobileRepository;
    public ReservationController(IAutomobileRepository automobileRepository)
    {
        _automobileRepository = automobileRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Reservation>>> GetReservations(int userId)
    {
        if (!await _automobileRepository.UserExistsAsync(userId))
        {
            return NotFound();
        }

        var reservations = await _automobileRepository.GetReservationsForUserAsync(userId);

        return Ok(reservations);
    }
    
    [HttpPost]
    public async Task<ActionResult<bool>> AddReservation(int userId, AddReservationDTO reservation)
    {

        var transaction = _automobileRepository.BeginTransaction(System.Data.IsolationLevel.RepeatableRead);

        var newReservation = new Reservation()
        {
            CatalogItemId = reservation.CatalogItemId,

            UserId = reservation.UserId,

            BeginTime = reservation.BeginTime,

            EndTime = reservation.EndTime,

            DriversDetails = new DriversDetails() {
                Name = reservation.DriversDetails.Name,
                Surname = reservation.DriversDetails.Surname,
                Country = reservation.DriversDetails.Country,
                Number = reservation.DriversDetails.Number
            }
        };

        var catalogItem = await _automobileRepository.GetItemByQuery(item => item.Id == newReservation.CatalogItemId);

        if (catalogItem == null)
            return NotFound(false);

        IEnumerable<CatalogItem>? availableItems = await _automobileRepository
            .GetMatchingCatalogItemsAsync(
                newReservation.BeginTime,
                (newReservation.EndTime - newReservation.BeginTime).Hours,
                catalogItem.LocationId
            );

        // bool isAvailable = availableItems.Any(i => i.Id == newReservation.Id);
        bool isAvailable = availableItems.Contains(catalogItem);

        if (!isAvailable)
            return Conflict(false);

        _automobileRepository.AddReservation(userId, newReservation);

        await _automobileRepository.SaveChangesAsync();

        transaction.Commit();

        return Ok(true);
    }

    
    [HttpDelete("{reservationId}")]
    public async Task<ActionResult<bool>> DeleteReservations(
        int userId,
        int reservationId
    )
    {
            if (!await _automobileRepository.UserExistsAsync(userId))
            {
                return NotFound();
            }

            var reservation = await _automobileRepository.GetSingleReservationForUserAsync(userId, reservationId);

            if (reservation == null)
            {
                return NotFound();
            }

            if (!CanDeleteReservation(reservation))
            {
                return Forbid();
            }

            await _automobileRepository.DeleteReservation(reservation);

            await _automobileRepository.SaveChangesAsync();

            return NoContent();
    }

    private bool CanDeleteReservation(Reservation reservation) {
        if (reservation.BeginTime > DateTime.UtcNow + new TimeSpan(48, 0, 0)) {
            return true;
        }

        return false;
    }
}
