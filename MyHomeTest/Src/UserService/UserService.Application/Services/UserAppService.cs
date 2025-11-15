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
    public class UserAppService : IUserAppService
    {
        private readonly UserDbContext _context;
        private readonly KafkaProducer _producer;

        public UserAppService(UserDbContext context, KafkaProducer producer)
        {
            _context = context;
            _producer = producer;
        }

        public async Task<UserDto> CreateUserAsync(string name, string email)
        {
            var user = new User(name, email);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var @event = new UserCreatedEvent(user.Id, user.Name, user.Email);
            await _producer.ProduceAsync("user-created", JsonSerializer.Serialize(@event));

            return new UserDto { Id = user.Id, Name = user.Name, Email = user.Email };
        }

        public async Task<UserDto> GetUserAsync(Guid id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null) return null;

            return new UserDto { Id = user.Id, Name = user.Name, Email = user.Email };
        }
    }
}
