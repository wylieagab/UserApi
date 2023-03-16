using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using UserApi.Models;
using UserApi.Models.Extensions;
using UserApi.Services;

namespace UserApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpGet]
        [EnableRateLimiting("LimitPolicy")]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(await _userService.GetAllAsync());
        }

        [HttpGet("{id}")]
        [EnableRateLimiting("LimitPolicy")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _userService.GetByIdAsync(id);

            if(user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost]
        [EnableRateLimiting("LimitPolicy")]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userExists = await _userService.DoesUserExistsWithEmail(user.Email);

            if(userExists)
            {
                return new BadRequestWithReasonResult("This email is already in use.");
            }

            await _userService.CreateAsync(user);

            return CreatedAtAction(nameof(GetUser), new { Id = user.Id }, user);
        }

        [HttpPut("{id}")]
        [EnableRateLimiting("LimitPolicy")]
        public async Task<IActionResult> UpdateUser(User user)
        {

            if(!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }

            await _userService.UpdateAsync(user);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [EnableRateLimiting("LimitPolicy")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var userDeleted = await _userService.DeleteAsync(id);
            
            if (userDeleted == null) return NotFound();

            return NoContent();
        }
    }
}
