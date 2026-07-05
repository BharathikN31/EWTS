using EWTS.Application.DTOs;

namespace EWTS.Application.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> RegisterAsync(CreateUserDto dto);
        Task<string> LoginAsync(LoginDto dto);
        Task<UserDto> GetByIdAsync(Guid id);
        Task<List<UserDto>> GetAllAsync();
        Task<UserDto> CreateUserWithRoleAsync(CreateUserWithRoleDto dto);   // 🔹 NEW — Admin-only
        Task<bool> HasAnyUsersAsync();                                      // 🔹 NEW — bootstrap check
        Task<UserDto> BootstrapFirstAdminAsync(CreateUserDto dto);          // 🔹 NEW — one-time setup
    }
}