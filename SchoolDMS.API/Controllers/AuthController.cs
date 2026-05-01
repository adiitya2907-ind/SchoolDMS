using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolDMS.API.Models.DTOs.Auth;
using SchoolDMS.API.Services.Interfaces;

namespace SchoolDMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO request)
        {
            var result = await _authService.LoginAsync(request);
            if (result.Success)
            {
                return Ok(result);
            }
            return Unauthorized(result);
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterDTO request)
        {
            var result = await _authService.RegisterAsync(request);
            if (result.Success)
            {
                return CreatedAtAction(nameof(Register), new { id = result.Data }, result);
            }
            return BadRequest(result);
        }
    }
}
