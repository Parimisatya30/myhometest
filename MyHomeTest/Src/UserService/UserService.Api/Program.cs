using Microsoft.EntityFrameworkCore;
using UserService.Application.Interfaces;
using UserService.Application.Services;
using UserService.Infrastructure.Data;
using UserService.Infrastructure.Kafka;

var builder = WebApplication.CreateBuilder(args);

/// ------------------------------------------------------------
/// Service Configuration
/// ------------------------------------------------------------

/// <summary>
/// Registers the EF Core DbContext using an in-memory database.
/// This is ideal for demos, tests, or lightweight setups.
/// </summary>
builder.Services.AddDbContext<UserDbContext>(options =>
    options.UseInMemoryDatabase("UsersDb"));

/// <summary>
/// Registers a Kafka producer as a singleton since producers are
/// thread-safe and should be reused throughout the lifetime of
/// the application.
/// </summary>
builder.Services.AddSingleton(new KafkaProducer("kafka:9092"));

/// <summary>
/// Registers the User Application Service which contains the
/// business logic for creating and retrieving users.
/// </summary>
builder.Services.AddScoped<IUserAppService, UserAppService>();

/// <summary>
/// Adds MVC controller support.
/// </summary>
builder.Services.AddControllers();

/// <summary>
/// Adds Swagger/OpenAPI generation for API documentation.
/// </summary>
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


/// ------------------------------------------------------------
/// Middleware Pipeline
/// ------------------------------------------------------------

var app = builder.Build();

/// <summary>
/// Enables Swagger UI and OpenAPI documentation.
/// Useful for testing APIs during development.
/// </summary>
app.UseSwagger();
app.UseSwaggerUI();

/// <summary>
/// Redirects HTTP requests to HTTPS for better security.
/// </summary>
app.UseHttpsRedirection();

/// <summary>
/// Adds authorization middleware (not strictly required unless
/// future endpoints are protected).
/// </summary>
app.UseAuthorization();

/// <summary>
/// Maps all API controllers.
/// </summary>
app.MapControllers();

/// <summary>
/// Runs the application.
/// </summary>
app.Run();
