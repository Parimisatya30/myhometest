using Microsoft.AspNetCore.Mvc;
using OrderService.Application.Interfaces;

namespace OrderService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderAppService _orderService;
        public OrdersController(IOrderAppService orderService) => _orderService = orderService;

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrderRequest request)
        {
            var order = await _orderService.CreateOrderAsync(request.UserId, request.Product, request.Quantity, request.Price);
            return Ok(order);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var order = await _orderService.GetOrderAsync(id);
            return order == null ? NotFound() : Ok(order);
        }
    }

    public class CreateOrderRequest
    {
        public Guid UserId { get; set; }
        public string Product { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
