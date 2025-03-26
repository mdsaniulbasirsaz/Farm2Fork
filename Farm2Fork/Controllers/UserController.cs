using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Farm2Fork.Models;
using Farm2Fork.Services.Interfaces; // Add this
using System.Collections.Generic;
using Farm2Fork.Data;
using Farm2Fork.DTOs;

namespace Farm2Fork.Controllers
{
    [Route("api/[controller]/v1")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/user
        [HttpGet]
        public async Task<ActionResult> GetUsers()
        {
            return Ok(
                await _userService.GetUsersAsync()
            );
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                await _userService.RegisterUserAsync(registerDto);
                return Ok("User registered successfully.");
            }
            catch (InvalidOperationException ex)
            {
                // Return a bad request with the exception message
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var authenticatedUser = await _userService.AuthenticateUserAsync(loginDto.Email, loginDto.Password);
            if (authenticatedUser == null) return Unauthorized("Invalid credentials");
            
            return Ok(authenticatedUser);
        }
    }
}
