using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using UserService.Domain.Entities;

namespace UserService.Infrastructure.Data
{
    public class UserDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }
    }
}
