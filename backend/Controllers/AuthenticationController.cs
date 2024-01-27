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

            CookieHelper.SignIn(HttpContext, user);

            string token = JsonHelper.GetAppUserToken(user);

            return Ok(token);
        }

        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok();
        }

        [HttpGet("ping")]
        public ActionResult<bool> Ping()
        {
            if (User.Identity.IsAuthenticated)
            {
                var authenticationExpiresUtc = User.FindFirstValue(ClaimTypes.Expiration);

                if (DateTime.TryParse(authenticationExpiresUtc, out DateTime expirationDateTime))
                {
                    if (expirationDateTime > DateTime.UtcNow)
                    {
                        return Ok(true);
                    }
                }
            }

            return Ok(false);
        }
    }
}