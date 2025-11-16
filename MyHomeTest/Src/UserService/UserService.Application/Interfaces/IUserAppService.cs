using UserService.Application.DTOs;

namespace UserService.Application.Interfaces
{
    /// <summary>
    /// Defines the contract for the User Application Service.
    /// 
    /// Responsibilities:
    ///  - Encapsulates all user-related business logic.
    ///  - Provides a clean interface for controllers to interact with.
    ///  - Supports dependency injection for loose coupling.
    ///  - Ensures separation between API, application logic, and domain entities.
    /// </summary>
    public interface IUserAppService
    {
        /// <summary>
        /// Creates a new user and returns a DTO representation.
        /// Also responsible for triggering a UserCreated event (in the implementation).
        /// </summary>
        /// <param name="name">Full name of the user.</param>
        /// <param name="email">Email address of the user.</param>
        /// <returns>UserDto representing the created user.</returns>
        Task<UserDto> CreateUserAsync(string name, string email);

        /// <summary>
        /// Retrieves a user by their unique identifier.
        /// </summary>
        /// <param name="id">The user's unique ID.</param>
        /// <returns>UserDto if found, otherwise null.</returns>
        Task<UserDto> GetUserAsync(Guid id);
    }
}
