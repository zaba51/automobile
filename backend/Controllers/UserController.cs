using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using backend.Entities;
using backend.Helpers;
using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/users")]
    [Authorize]
    public class UsersController : ControllerBase
    {

        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        /// <summary>
        /// GetSingleUser
        /// </summary>
        /// <param name="id">Id of the user</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Requested user</response>
        /// <response code="401">Access without a valid token</response>
        /// <response code="403">User not authorized for this resource</response>
        /// <response code="404">User with provided id doesn't exist</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        [Route("{id}", Name = "GetSingleUser")]
        public async Task<ActionResult> GetSingleUser(
            int id)
        {
            var requestingUserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (Convert.ToInt32(requestingUserId) != id)
            {
                return Forbid();
            }

            var user = await _userRepository.GetSingleUserAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        /// <summary>
        /// AddUser
        /// </summary>
        /// <param name="user">AddUserDTO for creation</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Requested user</response>
        /// <response code="409">user with provided username already exists</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> AddUser(AddUserDTO user)
        {

            if (await _userRepository.GetUserByUsernameAsync(user.Email) != null)
            {
                return Conflict();
            }

            var userToAdd = new User()
            {
                Email = user.Email,
                Password = PasswordHasher.HashPassword(user.Password)
            };

            _userRepository.AddUser(userToAdd);

            await _userRepository.SaveChangesAsync();

            CookieHelper.SignIn(HttpContext, userToAdd);

            string token = JsonHelper.GetAppUserToken(userToAdd);

            return Ok(token);
        }
    }
}