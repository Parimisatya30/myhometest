using OrderService.Application.DTOs;

namespace OrderService.Application.Interfaces
{
    public interface IOrderAppService
    {
        Task<OrderDto> CreateOrderAsync(Guid userId, string product, int quantity, decimal price);
        Task<OrderDto> GetOrderAsync(Guid id);
    }
}
