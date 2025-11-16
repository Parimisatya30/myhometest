using OrderService.Application.DTOs;

namespace OrderService.Application.Interfaces
{
    /// <summary>
    /// Defines the contract for the Order Application Service.
    /// 
    /// Responsibilities:
    ///  - Encapsulates all order-related business logic.
    ///  - Provides a clean interface for controllers to interact with.
    ///  - Supports dependency injection for loose coupling.
    ///  - Ensures separation between API, application logic, and domain entities.
    ///  - Handles event publishing (e.g., OrderCreated) in the implementation.
    /// </summary>
    public interface IOrderAppService
    {
        /// <summary>
        /// Creates a new order and returns a DTO representation.
        /// Also responsible for triggering an OrderCreated event (in the implementation).
        /// </summary>
        /// <param name="userId">ID of the user placing the order.</param>
        /// <param name="product">Product name.</param>
        /// <param name="quantity">Quantity of the product.</param>
        /// <param name="price">Total price for the order.</param>
        /// <returns>OrderDto representing the created order.</returns>
        Task<OrderDto> CreateOrderAsync(Guid userId, string product, int quantity, decimal price);

        /// <summary>
        /// Retrieves an order by its unique identifier.
        /// </summary>
        /// <param name="id">The order's unique ID.</param>
        /// <returns>OrderDto if found, otherwise null.</returns>
        Task<OrderDto> GetOrderAsync(Guid id);
    }
}
