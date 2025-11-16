using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using UserService.Domain.Entities;

namespace UserService.Infrastructure.Data
{
    /// <summary>
    /// Entity Framework Core database context for the User Service.
    /// This class manages the connection to the database and provides
    /// access to User entities through DbSet properties.
    ///
    /// In this assignment, an in-memory database is used, but EF Core
    /// allows easy replacement with SQL Server, PostgreSQL, etc.,
    /// without modifying the domain or application layers.
    /// </summary>
    public class UserDbContext : DbContext
    {
        /// <summary>
        /// Represents the Users table in the database.
        /// EF Core automatically creates this table (or in-memory store)
        /// based on the User entity structure.
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Constructor used by ASP.NET Core dependency injection.
        /// Accepts database configuration (provider, connection string, etc.)
        /// and passes it to the base DbContext.
        /// </summary>
        public UserDbContext(DbContextOptions<UserDbContext> options)
            : base(options)
        {
        }
    }
}
