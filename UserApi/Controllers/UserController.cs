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
        [ProducesResponseType(typeof(IEnumerable<UserDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(await _userService.GetAllAsync());
        }

        [HttpGet("{id}")]
        [EnableRateLimiting("LimitPolicy")]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _userService.GetByIdAsync(id);

            if(user == null)
            {
                return NotFound();
            }

            return Ok(UserMapper.ToUserDto(user));
        }

        [HttpPost]
        [EnableRateLimiting("LimitPolicy")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateUser([FromBody] UserDto userDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = UserMapper.ToUser(userDto);
            var userExists = await _userService.DoesUserExistsWithEmail(user.Email);

            if(userExists)
            {
                return new BadRequestWithReasonResult("This email is already in use.");
            }

            await _userService.CreateAsync(user);

            return CreatedAtAction(nameof(GetUser), new { Id = user.Id }, UserMapper.ToUserDto(user));
        }

        [HttpPut("{id}")]
        [EnableRateLimiting("LimitPolicy")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateUser(UserDto userDto)
        {

            if(!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }

            var user = UserMapper.ToUser(userDto);
            await _userService.UpdateAsync(user);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [EnableRateLimiting("LimitPolicy")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var userDeleted = await _userService.DeleteAsync(id);
            
            if (userDeleted == null) return NotFound();

            return NoContent();
        }
    }
}
