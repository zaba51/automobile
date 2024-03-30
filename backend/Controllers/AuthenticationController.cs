using System.Buffers.Text;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using automobile.Models;
using backend.Entities;
using backend.Helpers;
using backend.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;


namespace cineman.API.Controllers
{
    [ApiController]
    [Route("api/authentication")]
    public class AuthenticationController : ControllerBase
    {

        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public AuthenticationController(IUserRepository automobileRepository,
        IConfiguration configuration)
        {
            _userRepository = automobileRepository ?? throw new ArgumentNullException(nameof(automobileRepository));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public class UserLoginBody
        {
            public string? Email { get; set; }
            public string? Password { get; set; }
        }

        /// <summary>
        /// Login (authenticate)
        /// </summary>
        /// <param name="userLoginBody">UserLoginBody to login</param>
        /// <returns>ActionResult/<string/></returns>
        /// <response code="200">Logged in succesfully</response>
        /// <response code="401">Invalid credentials</response>
        /// <response code="404">User not found</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPost("authenticate")]
        public async Task<ActionResult<string>> Login(UserLoginBody userLoginBody)
        {
            var user = await _userRepository.GetUserByQuery(user => user.Email == userLoginBody.Email);

            if (user == null)
            {
                return NotFound();
            }

            bool isPasswordValid = PasswordHasher.VerifyPassword(userLoginBody.Password, user.Password);

            if (!isPasswordValid)
            {
                return Unauthorized();
            }

            string token = JWTHelper.SignIn(user, _configuration);

            return Ok(token);
        }

        /// <summary>
        /// Logout
        /// </summary>
        /// <returns>ActionResult</returns>
        /// <response code="200">Logged out succesfully</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok();
        }
    }
}