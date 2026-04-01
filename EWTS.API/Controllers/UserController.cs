using EWTS.Application.DTOs;
using EWTS.Application.Interfaces;
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

        // 🔹 REGISTER
        [HttpPost("register")]
        public async Task<IActionResult> Register(CreateUserDto dto)
        {
            var result = await _userService.RegisterAsync(dto);
            return Ok(result);
        }

        // 🔹 LOGIN
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var result = await _userService.LoginAsync(dto);
            return Ok(result);
        }

        // 🔹 GET USER
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _userService.GetByIdAsync(id);
            return Ok(result);
        }
    }
}