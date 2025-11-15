using Microsoft.EntityFrameworkCore;
using OrderService.Domain.Entities;
using System.Collections.Generic;

namespace OrderService.Infrastructure.Data
{
    public class OrderDbContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options) { }
    }
}
