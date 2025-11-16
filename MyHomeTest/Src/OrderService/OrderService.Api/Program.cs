using Microsoft.EntityFrameworkCore;
using OrderService.Application.Interfaces;
using OrderService.Application.Services;
using OrderService.Infrastructure.Data;
using OrderService.Infrastructure.Kafka;

var builder = WebApplication.CreateBuilder(args);


// -----------------------------
// Database (In-Memory Database)
// -----------------------------
// Registers OrderDbContext using EF Core InMemory database.
// This keeps the assignment simple and avoids requiring SQL Server.
// Each service maintains its own database because microservices
// must have autonomous persistence.
builder.Services.AddDbContext<OrderDbContext>(options => options.UseInMemoryDatabase("OrdersDb"));

// -----------------------------
// Kafka Producer Registration
// -----------------------------
// Registers KafkaProducer as a Singleton because:
// - Producer is thread-safe
// - Kafka client recommends single instance per app
// Bootstrap server points to the Kafka container in docker-compose.
builder.Services.AddSingleton(new KafkaProducer("kafka:9092"));

// -----------------------------
// Application Layer Services
// -----------------------------
// Registers the Order application's business logic service.
// Scoped lifetime means: one instance per HTTP request.
builder.Services.AddScoped<IOrderAppService, OrderAppService>();

// -----------------------------
// Controllers and Swagger
// -----------------------------
// Adds API controllers.
// Adds Swagger for API testing/documentation (useful for interviews).
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Enable Swagger UI for easy testing of APIs from browser.
app.UseSwagger();
app.UseSwaggerUI();

// Redirect HTTP → HTTPS for security.
app.UseHttpsRedirection();

// Authorization middleware (not used heavily in assignment).;
app.UseAuthorization();
// Maps controller routes to endpoints.
app.MapControllers();

// Starts the OrderService application.
app.Run();

