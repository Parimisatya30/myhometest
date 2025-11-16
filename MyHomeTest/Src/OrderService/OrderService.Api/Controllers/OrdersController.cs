using Microsoft.AspNetCore.Mvc;
using OrderService.Application.Interfaces;

namespace OrderService.Api.Controllers
{
    //
    // ------------------------------------------------------------
    // OrdersController
    // ------------------------------------------------------------
    //
    // This controller exposes the public HTTP API endpoints for the
    // Order microservice. It handles incoming requests from clients
    // and delegates the business logic to the application layer
    // (IOrderAppService), following proper Clean Architecture patterns.
    //
    // Responsibilities:
    //  - Accept API requests for order creation and retrieval
    //  - Validate incoming request models (automatically via ASP.NET)
    //  - Call the application service to perform use cases
    //  - Return appropriate HTTP responses
    //
    // Does NOT:
    //  - Contain business logic
    //  - Talk directly to the database
    //  - Work with Kafka directly
    //
    // This keeps the controller thin and focused on API concerns only.
    //

    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderAppService _orderService;

        //
        // Constructor Injection
        // ---------------------
        // ASP.NET resolves IOrderAppService from DI.
        // This follows Dependency Inversion and makes unit testing easy
        // because the service can be mocked.
        //
        public OrdersController(IOrderAppService orderService)
        {
            _orderService = orderService;
        }

        //
        // ------------------------------------------------------------
        // POST /orders
        // ------------------------------------------------------------
        //
        // Creates a new order.
        // Request body → CreateOrderRequest
        //
        // Flow:
        // 1. Controller receives JSON request
        // 2. Delegates to CreateOrderAsync in the application layer
        // 3. Application layer:
        //      - Validates user input
        //      - Creates order
        //      - Saves to database
        //      - Publishes "OrderCreated" Kafka event
        // 4. Returns the created order DTO
        //
        // Returns:
        // 200 OK → Order created successfully
        //
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrderRequest request)
        {
            var order = await _orderService.CreateOrderAsync(
                request.UserId,
                request.Product,
                request.Quantity,
                request.Price
            );

            return Ok(order);
        }

        //
        // ------------------------------------------------------------
        // GET /orders/{id}
        // ------------------------------------------------------------
        //
        // Retrieves order details by orderId.
        //
        // Flow:
        // 1. Extract orderId from route
        // 2. Call GetOrderAsync in the application layer
        // 3. Service returns DTO or null
        // 4. Controller returns:
        //      - 200 OK with order data
        //      - 404 NotFound if order doesn't exist
        //
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var order = await _orderService.GetOrderAsync(id);
            return order == null ? NotFound() : Ok(order);
        }
    }

    //
    // ------------------------------------------------------------
    // CreateOrderRequest DTO
    // ------------------------------------------------------------
    //
    // Represents the JSON request body for creating an order.
    // Controllers and application services use this DTO to avoid
    // binding HTTP models directly to domain models.
    //
    // Fields:
    //  - UserId   → User placing the order
    //  - Product  → Item being purchased
    //  - Quantity → Number of units
    //  - Price    → Total price for the order
    //
    public class CreateOrderRequest
    {
        public Guid UserId { get; set; }
        public string Product { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
