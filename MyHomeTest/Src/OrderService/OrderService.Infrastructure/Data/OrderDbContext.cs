using Microsoft.EntityFrameworkCore;
using OrderService.Domain.Entities;

namespace OrderService.Infrastructure.Data
{
    /// <summary>
    /// Entity Framework Core database context for the Order Service.
    /// Manages access to the Orders table and tracks entity changes.
    /// </summary>
    public class OrderDbContext : DbContext
    {
        /// <summary>
        /// Represents the Orders collection/table in the database.
        /// </summary>
        public DbSet<Order> Orders { get; set; }

        /// <summary>
        /// Initializes the DbContext using the provided configuration options.
        /// </summary>
        /// <param name="options">Database provider and configuration settings.</param>
        public OrderDbContext(DbContextOptions<OrderDbContext> options)
            : base(options)
        {
            // Additional configuration can be added here if needed.
        }
    }
}