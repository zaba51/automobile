// using backend.Services;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Mvc;

// namespace backend.Controllers
// {
//     [ApiController]
//     [Route("api/users")]
//     [Authorize]
//     public class UsersController : ControllerBase
//     {

//         private readonly IAutomobileRepository _automobileRepository;

//         public UsersController(IAutomobileRepository automobileRepository)
//         {
//             _automobileRepository = automobileRepository ?? throw new ArgumentNullException(nameof(automobileRepository));
//         }

//         [HttpGet]
//         [Route("{id}", Name = "GetSingleUser")]
//         public async Task<ActionResult> GetSingleUser(
//             int id)
//         {
//             // var user = await _automobileRepository.GetSingleUserAsync(id);

//             // if (user == null)
//             // {
//             //     return NotFound();
//             // }

//             // return Ok(_mapper.Map<GetUserDTO>(user));
//             return Ok();
//         }
        
//         [HttpPost]
//         [AllowAnonymous]
//         public async Task<ActionResult> AddUser(AddUserDTO user)
//         {

//             var finalUSer = _mapper.Map<Entities.User>(user);

//             if (await _cinemanRepository.GetUserByUsernameAsync(finalUSer.Email) != null)
//             {
//                 return Conflict();
//             }

//             _cinemanRepository.AddUser(finalUSer);

//             await _cinemanRepository.SaveChangesAsync();

//             var addedUserToReturn = _mapper.Map<Models.GetUserDTO>(finalUSer);

//             return CreatedAtRoute("GetSingleUser",
//                 new
//                 {
//                     id = finalUSer.Id,
//                 },
//                 addedUserToReturn
//             );
//         }

//         /// <summary>
//         /// Patch user
//         /// </summary>
//         /// <param name="userId">User's id</param>
//         /// <param name="patchDocument">PatchDocument</param>
//         /// <returns>ActionResult</returns>
//         [ProducesResponseType(StatusCodes.Status204NoContent)]
//         [ProducesResponseType(StatusCodes.Status404NotFound)]
//         [ProducesResponseType(StatusCodes.Status400BadRequest)]
//         [HttpPatch("{userId}")]
//         [AllowAnonymous]
//         public async Task<ActionResult> PatchUser(int userId,
//         JsonPatchDocument<PatchUserDTO> patchDocument
//         )
//         {

//             var user = await _cinemanRepository.GetSingleUserAsync(userId);

//             if (user == null)
//             {
//                 return NotFound();
//             }

//             var userToPatch = _mapper.Map<PatchUserDTO>(user);

//             patchDocument.ApplyTo(userToPatch, ModelState);

//             if (!ModelState.IsValid)
//             {
//                 return BadRequest(ModelState);
//             }

//             if (!TryValidateModel(userToPatch))
//             {
//                 return BadRequest(ModelState);
//             }

//             _mapper.Map(userToPatch, user);

//             await _cinemanRepository.SaveChangesAsync();


//             return NoContent();
//         }
//     }
// }