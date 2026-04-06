using EWTS.Application.DTOs;
using EWTS.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EWTS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // 🔹 REGISTER (PUBLIC)
        [HttpPost("register")]
        public async Task<IActionResult> Register(CreateUserDto dto)
        {
            var result = await _userService.RegisterAsync(dto);
            return Ok(result);
        }

        // 🔹 LOGIN (PUBLIC)
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var token = await _userService.LoginAsync(dto);
            return Ok(new { token });
        }

        // 🔹 ANY LOGGED USER
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _userService.GetByIdAsync(id);
            return Ok(result);
        }

        // 🔹 ADMIN ONLY
        [Authorize(Roles = "Admin")]
        [HttpGet("admin")]
        public IActionResult AdminOnly()
        {
            return Ok("Only Admin can access");
        }

        // 🔹 ADMIN + MANAGER
        [Authorize(Roles = "Admin,Manager")]
        [HttpGet("manager")]
        public IActionResult ManagerAccess()
        {
            return Ok("Admin or Manager can access");
        }
    }
}