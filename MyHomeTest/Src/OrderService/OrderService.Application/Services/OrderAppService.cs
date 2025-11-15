using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using OrderService.Application.DTOs;
using OrderService.Application.Interfaces;
using OrderService.Domain.Entities;
using OrderService.Infrastructure.Data;

using OrderService.Infrastructure.Kafka;
using Shared.Events;



namespace OrderService.Application.Services
{
    public class OrderAppService : IOrderAppService
    {
        private readonly OrderDbContext _context;
        private readonly KafkaProducer _producer;

        public OrderAppService(OrderDbContext context, KafkaProducer producer)
        {
            _context = context;
            _producer = producer;
        }

        public async Task<OrderDto> CreateOrderAsync(Guid userId, string product, int quantity, decimal price)
        {
            var order = new Order(userId, product, quantity, price);
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            var @event = new OrderCreatedEvent(order.Id, userId, product, quantity, price);
            await _producer.ProduceAsync("order-created", JsonSerializer.Serialize(@event));

            return new OrderDto { Id = order.Id, UserId = userId, Product = product, Quantity = quantity, Price = price };
        }

        public async Task<OrderDto> GetOrderAsync(Guid id)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);
            if (order == null) return null;

            return new OrderDto { Id = order.Id, UserId = order.UserId, Product = order.Product, Quantity = order.Quantity, Price = order.Price };
        }
    }
}
