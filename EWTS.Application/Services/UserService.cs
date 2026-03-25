using EWTS.Application.DTOs;
using EWTS.Application.Interfaces;

namespace EWTS.Application.Services
{
    public class UserService : IUserService
    {
        public Task<UserDto> RegisterAsync(CreateUserDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<UserDto> LoginAsync(LoginDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<UserDto> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}