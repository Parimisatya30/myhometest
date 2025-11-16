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
    //
    // ------------------------------------------------------------
    // OrderAppService
    // ------------------------------------------------------------
    //
    // This class contains all business logic for the Order microservice.
    //
    // Core responsibilities:
    //  - Create new orders
    //  - Persist orders using the EF Core DbContext
    //  - Publish OrderCreated events to Kafka
    //  - Retrieve order information
    //
    // This follows Clean Architecture:
    //  - Application layer is independent of controllers
    //  - Domain entities remain pure
    //  - Infrastructure (DbContext, Kafka) is injected as dependencies
    //
    // Why this matters:
    //  - Keeps logic reusable/testable
    //  - Controller stays thin
    //  - Kafka and Database can be swapped/mocked
    //
    public class OrderAppService : IOrderAppService
    {
        private readonly OrderDbContext _context;
        private readonly KafkaProducer _producer;

        //
        // Constructor Injection
        // ---------------------
        // DbContext & KafkaProducer are injected from DI.
        // This decouples the application layer from the underlying
        // persistence and messaging implementations.
        //
        public OrderAppService(OrderDbContext context, KafkaProducer producer)
        {
            _context = context;
            _producer = producer;
        }

        //
        // ------------------------------------------------------------
        // CreateOrderAsync
        // ------------------------------------------------------------
        //
        // Creates a new order and publishes an "OrderCreated" Kafka event.
        //
        // Steps:
        // 1. Instantiate the domain entity (Order)
        // 2. Add to EF Core DbContext
        // 3. Save to the in-memory database
        // 4. Construct a strongly typed event object
        // 5. Serialize event and publish to Kafka
        // 6. Return an OrderDto to the API layer
        //
        // Why events?
        //  - OrderService informs other services (e.g., Notification, Billing)
        //  - Demonstrates event-driven architecture
        //  - Services remain loosely coupled
        //
        public async Task<OrderDto> CreateOrderAsync(Guid userId, string product, int quantity, decimal price)
        {
            // 1. Create the domain entity (encapsulates invariants)
            var order = new Order(userId, product, quantity, price);

            // 2. Track with EF Core
            _context.Orders.Add(order);

            // 3. Persist in the in-memory store
            await _context.SaveChangesAsync();

            // 4. Create event payload
            var @event = new OrderCreatedEvent(
                order.Id,
                userId,
                product,
                quantity,
                price
            );

            // 5. Publish the event to Kafka topic: "order-created"
            await _producer.ProduceAsync(
                "order-created",
                JsonSerializer.Serialize(@event)
            );

            // 6. Return a DTO to isolate API from domain models
            return new OrderDto
            {
                Id = order.Id,
                UserId = userId,
                Product = product,
                Quantity = quantity,
                Price = price
            };
        }

        //
        // ------------------------------------------------------------
        // GetOrderAsync
        // ------------------------------------------------------------
        //
        // Retrieves a single order by ID.
        //
        // Steps:
        //  - Query EF Core for entity
        //  - If not found → return null
        //  - Map entity → OrderDto
        //
        // Why DTOs?
        //  - Avoid exposing domain models directly
        //  - Future-proofing for API versioning
        //
        public async Task<OrderDto> GetOrderAsync(Guid id)
        {
            // Query the in-memory database
            var order = await _context.Orders
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null) return null;

            // Map to DTO
            return new OrderDto
            {
                Id = order.Id,
                UserId = order.UserId,
                Product = order.Product,
                Quantity = order.Quantity,
                Price = order.Price
            };
        }
    }
}
