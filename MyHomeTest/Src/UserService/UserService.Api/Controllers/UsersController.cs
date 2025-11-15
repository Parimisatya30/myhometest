using Microsoft.AspNetCore.Mvc;
using UserService.Application.Interfaces;

namespace UserService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserAppService _userAppService;
        public UsersController(IUserAppService userService) => _userAppService = userService;

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserRequest request)
        {
            var user = await _userAppService.CreateUserAsync(request.Name, request.Email);
            return Ok(user);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var user = await _userAppService.GetUserAsync(id);
            return user == null ? NotFound() : Ok(user);
        }
    }

    public class CreateUserRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
