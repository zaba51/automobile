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

        [HttpGet]
        [Route("{id}", Name = "GetSingleUser")]
        public async Task<ActionResult> GetSingleUser(
            int id)
        {
            var user = await _userRepository.GetSingleUserAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }
        
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