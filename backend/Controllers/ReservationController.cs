using backend.Entities;
using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/reservations")]
public class ReservationController : ControllerBase
{
    
    private readonly IAutomobileRepository _automobileRepository;
    public ReservationController(IAutomobileRepository automobileRepository)
    {
        _automobileRepository = automobileRepository;
    }

    [HttpGet]
    [Route("{userId}")]
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
    [Route("{userId}")]
    public async Task<ActionResult> AddReservation(int userId, AddReservationDTO reservation)
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

        return Ok();

    }
}
