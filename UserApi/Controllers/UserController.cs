﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using UserApi.Models.Constants;
using UserApi.Models.Dtos;
using UserApi.Models.Extensions;
using UserApi.Services;
using UserApi.Validators;

namespace UserApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IUserValidator _userValidator;

        public UserController(IUserService userService, IUserValidator userValidator)
        {
            _userService = userService;
            _userValidator = userValidator;
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
            var userDto = await _userService.GetByIdAsync(id);

            if(userDto == null)
            {
                return NotFound();
            }

            return Ok(userDto);
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

            var (isUserValid, responseMessage) = await _userValidator.ValidateEmailUniquenessAsync(userDto);
            if(!isUserValid)
            {
                return new BadRequestWithReasonResult(responseMessage);
            }

            var createdUser = await _userService.CreateAsync(userDto);

            return CreatedAtAction(nameof(GetUser), new { Id = createdUser.Id }, createdUser);
        }

        [HttpPut("{id}")]
        [EnableRateLimiting("LimitPolicy")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateUser(int id, UserDto userDto)
        {

            if(!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }

            var (isValidUser, responseMessage) = await _userValidator.ValidateUserPutRequestAsync(id, userDto);
            if(!isValidUser)
            {
                return new BadRequestWithReasonResult(responseMessage);
            }

            await _userService.UpdateAsync(userDto);

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
