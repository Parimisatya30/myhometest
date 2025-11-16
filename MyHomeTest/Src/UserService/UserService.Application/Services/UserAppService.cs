using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using UserService.Application.DTOs;
using UserService.Application.Interfaces;
using UserService.Domain.Entities;
using UserService.Infrastructure.Data;
using UserService.Infrastructure.Kafka;
using Shared.Events;

namespace UserService.Application.Services
{
    /// <summary>
    /// Application service for handling all user-related business logic.
    /// Implements IUserAppService to allow loose coupling with controllers.
    /// Responsibilities:
    ///  - Create users
    ///  - Persist users to the in-memory database
    ///  - Publish "UserCreated" events to Kafka
    ///  - Retrieve users by ID
    /// </summary>
    public class UserAppService : IUserAppService
    {
        private readonly UserDbContext _context;
        private readonly KafkaProducer _producer;

        /// <summary>
        /// Constructor: Injects DbContext and Kafka producer via dependency injection.
        /// This keeps the application layer decoupled from infrastructure details.
        /// </summary>
        public UserAppService(UserDbContext context, KafkaProducer producer)
        {
            _context = context;
            _producer = producer;
        }

        /// <summary>
        /// Creates a new user and publishes a "UserCreated" event to Kafka.
        /// </summary>
        /// <param name="name">Full name of the user.</param>
        /// <param name="email">Email address of the user.</param>
        /// <returns>A UserDto representing the created user.</returns>
        public async Task<UserDto> CreateUserAsync(string name, string email)
        {
            // 1. Create domain entity
            var user = new User(name, email);

            // 2. Add to EF Core in-memory database
            _context.Users.Add(user);

            // 3. Persist changes
            await _context.SaveChangesAsync();

            // 4. Create event payload for Kafka
            var @event = new UserCreatedEvent(user.Id, user.Name, user.Email);

            // 5. Serialize and send the event to the "user-created" topic
            await _producer.ProduceAsync("user-created", JsonSerializer.Serialize(@event));

            // 6. Return DTO to API layer (controller)
            return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email
            };
        }

        /// <summary>
        /// Retrieves a user by their unique ID.
        /// </summary>
        /// <param name="id">The ID of the user.</param>
        /// <returns>UserDto if found, otherwise null.</returns>
        public async Task<UserDto> GetUserAsync(Guid id)
        {
            // Query the in-memory database for the user
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

            // Return null if user does not exist (controller handles 404)
            if (user == null) return null;

            // Map domain entity to DTO for API layer
            return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email
            };
        }
    }
}
