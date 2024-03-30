using System.Security.Claims;
using backend.Entities;
using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

/// <summary>
/// Controller for managing reservations for a specific user.
/// </summary>
[ApiController]
[Authorize]
[Route("api/users/{userId}/reservations")]
public class ReservationController : ControllerBase
{
    
    private readonly IReservationRepository _reservationRepository;
    private readonly IUserRepository _userRepository;
    private readonly ICatalogRepository _catalogRepository;
    public ReservationController(
        IReservationRepository reservationRepository,
        IUserRepository userRepository,
        ICatalogRepository catalogRepository
    )
    {
        _userRepository = userRepository;
        _reservationRepository = reservationRepository;
        _catalogRepository = catalogRepository;
    }
    
    /// <summary>
    /// GetReservations
    /// </summary>
    /// <param name="userId">Id of the user</param>
    /// <returns>Reservations</returns>
    /// <response code="200">Returned user's reservations</response>
    /// <response code="401">Unauthorized access</response>
    /// <response code="403">Forbidden access to resource</response>
    /// <response code="404">User not found</response>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Reservation>>> GetReservations(int userId)
    {
        if (!await _userRepository.UserExistsAsync(userId))
        {
            return NotFound();
        }

        var requestingUserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

        if (Convert.ToInt32(requestingUserId) != userId)
        {
            return Forbid();
        }

        var reservations = await _reservationRepository.GetReservationsForUserAsync(userId);

        return Ok(reservations);
    }

    /// <summary>
    /// AddReservation
    /// </summary>
    /// <param name="userId">Id of the user</param>
    /// <param name="reservation">AddReservationDTO specifing reservation details </param>
    /// <returns>An ActionResult with boolean</returns>
    /// <response code="200">Succesfully added reservation</response>
    /// <response code="401">Unauthorized access</response>
    /// <response code="403">Forbidden access to resource</response>
    /// <response code="404">User or item not found</response>
    /// <response code="409">Item is no longer available</response>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [HttpPost]
    public async Task<ActionResult<bool>> AddReservation(int userId, AddReservationDTO reservation)
    {
        if (!await _userRepository.UserExistsAsync(userId))
        {
            return NotFound();
        }

        var requestingUserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

        if (Convert.ToInt32(requestingUserId) != userId)
        {
            return Forbid();
        }

        var transaction = _reservationRepository.BeginTransaction(System.Data.IsolationLevel.RepeatableRead);

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

        var catalogItem = await _catalogRepository.GetItemByQuery(item => item.Id == newReservation.CatalogItemId);

        if (catalogItem == null)
            return NotFound(false);

        IEnumerable<CatalogItem>? availableItems = await _catalogRepository
            .GetMatchingCatalogItemsAsync(
                newReservation.BeginTime,
                (newReservation.EndTime - newReservation.BeginTime).Hours,
                catalogItem.LocationId
            );

        // bool isAvailable = availableItems.Any(i => i.Id == newReservation.Id);
        bool isAvailable = availableItems.Contains(catalogItem);

        if (!isAvailable)
            return Conflict(false);

        _reservationRepository.AddReservation(userId, newReservation);

        await _reservationRepository.SaveChangesAsync();

        transaction.Commit();

        return Ok(true);
    }

    /// <summary>
    /// DeleteReservations
    /// </summary>
    /// <param name="userId">Id of the user</param>
    /// <param name="reservationId">Id of the reservation</param>
    /// <returns>An ActionResult with boolean</returns>
    /// <response code="204">Succesfully deleted reservation</response>
    /// <response code="401">Unauthorized access</response>
    /// <response code="403">Forbidden access to resource or reservation can no longer be deleted</response>
    /// <response code="404">User or item not found</response>
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpDelete("{reservationId}")]
    public async Task<ActionResult<bool>> DeleteReservations(
        int userId,
        int reservationId
    )
    {
            if (!await _userRepository.UserExistsAsync(userId))
            {
                return NotFound();
            }

            var requestingUserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (Convert.ToInt32(requestingUserId) != userId)
            {
                return Forbid();
            }

            var reservation = await _reservationRepository.GetSingleReservationForUserAsync(userId, reservationId);

            if (reservation == null)
            {
                return NotFound();
            }

            if (!CanDeleteReservation(reservation))
            {
                return Forbid();
            }

            await _reservationRepository.DeleteReservation(reservation);

            await _reservationRepository.SaveChangesAsync();

            return NoContent();
    }

    private bool CanDeleteReservation(Reservation reservation) {
        if (reservation.BeginTime > DateTime.UtcNow + new TimeSpan(48, 0, 0)) {
            return true;
        }

        return false;
    }
}
