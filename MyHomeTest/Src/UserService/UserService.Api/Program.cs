using Microsoft.EntityFrameworkCore;
using UserService.Application.Interfaces;
using UserService.Application.Services;
using UserService.Infrastructure.Data;
using UserService.Infrastructure.Kafka;

var builder = WebApplication.CreateBuilder(args);

// Add DbContext (in-memory)
builder.Services.AddDbContext<UserDbContext>(options => options.UseInMemoryDatabase("UsersDb"));

// Kafka producer
builder.Services.AddSingleton(new KafkaProducer("kafka:9092"));

// Application services
builder.Services.AddScoped<IUserAppService, UserAppService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
