using EWTS.Application.DTOs;

namespace EWTS.Application.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> RegisterAsync(CreateUserDto dto);
        Task<string> LoginAsync(LoginDto dto);
        Task<UserDto> GetByIdAsync(Guid id);
    }
}