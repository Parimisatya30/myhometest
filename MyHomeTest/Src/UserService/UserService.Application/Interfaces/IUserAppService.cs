using UserService.Application.DTOs;

namespace UserService.Application.Interfaces
{
    public interface IUserAppService
    {
        Task<UserDto> CreateUserAsync(string name, string email);
        Task<UserDto> GetUserAsync(Guid id);
    }
}
