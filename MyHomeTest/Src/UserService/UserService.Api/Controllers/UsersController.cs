using Microsoft.AspNetCore.Mvc;
using UserService.Application.Interfaces;

namespace UserService.Api.Controllers
{
    /// <summary>
    /// REST API controller responsible for handling user-related operations.
    /// Routes incoming HTTP requests to the User Application Service, ensuring
    /// a clean separation between API concerns and business logic.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserAppService _userAppService;

        /// <summary>
        /// Injects the User Application Service that handles all business logic.
        /// </summary>
        public UsersController(IUserAppService userService)
            => _userAppService = userService;

        /// <summary>
        /// Creates a new user and publishes a UserCreated event to Kafka.
        /// </summary>
        /// <param name="request">Payload containing Name and Email of the new user.</param>
        /// <returns>The created user DTO.</returns>
        /// <remarks>
        /// POST /api/users
        /// </remarks>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserRequest request)
        {
            var user = await _userAppService.CreateUserAsync(request.Name, request.Email);
            return Ok(user);
        }

        /// <summary>
        /// Retrieves a user by their unique identifier.
        /// </summary>
        /// <param name="id">The user ID.</param>
        /// <returns>User DTO if found, otherwise 404 Not Found.</returns>
        /// <remarks>
        /// GET /api/users/{id}
        /// </remarks>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var user = await _userAppService.GetUserAsync(id);
            return user == null ? NotFound() : Ok(user);
        }
    }

    /// <summary>
    /// Represents the payload received when creating a new user.
    /// </summary>
    public class CreateUserRequest
    {
        /// <summary>
        /// Name of the user to be created.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Email address of the new user.
        /// </summary>
        public string Email { get; set; }
    }
}
