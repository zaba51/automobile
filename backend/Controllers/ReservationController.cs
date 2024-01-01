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
        // if (!await _automobileRepository.UserExistsAsync(userId))
        // {
        //     return NotFound();
        // }

        // var requestingUserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

        // if (Convert.ToInt32(requestingUserId) != userId)
        // {
        //     return Forbid();
        // }

        var reservations = await _automobileRepository.GetReservationsForUserAsync(userId);

        return Ok(reservations);
    }
    
    [HttpPost]
    public async Task<ActionResult<bool>> AddReservation(int userId, AddReservationDTO reservation)
    {
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

        _automobileRepository.AddReservation(userId, newReservation);

        await _automobileRepository.SaveChangesAsync();

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

            // var requestingUserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;


            // if (Convert.ToInt32(requestingUserId) != userId)
            // {
            //     return Forbid();
            // }

            var reservation = await _automobileRepository.GetSingleReservationForUserAsync(userId, reservationId);

            if (reservation == null)
            {
                return NotFound();
            }

            
            if (!CanDeleteReservation(reservation))
            {
                // return NotFound();
            }

            await _automobileRepository.DeleteReservation(reservation);

            await _automobileRepository.SaveChangesAsync();

            return NoContent();
    }

    private bool CanDeleteReservation(Reservation reservation) {
        if (reservation.BeginTime + new TimeSpan(48, 0, 0) > DateTime.UtcNow) {
            return false;
        }

        return true;
    }
}
