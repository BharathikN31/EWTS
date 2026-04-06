using EWTS.Application.DTOs;
using EWTS.Application.Interfaces;
using EWTS.Domain.Entities;
using EWTS.Domain.Enums;
using EWTS.Domain.Interfaces;

namespace EWTS.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly PasswordHasher _passwordHasher;

        private readonly JwtService _jwtService;

        public UserService(IUserRepository userRepository, JwtService jwtService)
        {
            _userRepository = userRepository;
            _passwordHasher = new PasswordHasher();
            _jwtService = jwtService;
        }

        // 🔹 REGISTER
        public async Task<UserDto> RegisterAsync(CreateUserDto dto)
        {
            var existingUser = await _userRepository.GetByEmailAsync(dto.Email);

            if (existingUser != null)
                throw new Exception("User already exists");

            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Email = dto.Email,
                PasswordHash = _passwordHasher.Hash(dto.Password),
                Role = UserRole.Employee,
                CreatedAt = DateTime.UtcNow
            };

            await _userRepository.AddAsync(user);

            return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role
            };
        }

        // 🔹 LOGIN
public async Task<string> LoginAsync(LoginDto dto)
{
    var user = await _userRepository.GetByEmailAsync(dto.Email);

    if (user == null)
        throw new Exception("Invalid email or password");

    var isValid = _passwordHasher.Verify(dto.Password, user.PasswordHash);

    if (!isValid)
        throw new Exception("Invalid email or password");

    return _jwtService.GenerateToken(user);
}
        // 🔹 GET USER BY ID
        public async Task<UserDto> GetByIdAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
                throw new Exception("User not found");

            return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role
            };
        }
    }
}