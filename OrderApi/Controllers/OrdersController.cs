using Microsoft.AspNetCore.Mvc;
using OrderApi.Services;

namespace OrderApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrders()
        {
            var orders = await _orderService.GetOrdersAsync();

            if (orders == null || !orders.Any())
            {
                return NotFound("No orders found");
            }

            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> GetOrderById(Guid id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);

            if(order == null)
            {
                return NotFound($"Book with Id:{id} not found.");
            }
            return Ok(order);
        }

        [HttpPost]
        public async Task<ActionResult<OrderDto>> CreateOrder(OrderDto orderDto)
        {
            return null;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<OrderDto>> UpdateOrder(Guid id, OrderDto orderDto)
        {
            if(orderDto == null) 
            {
                return BadRequest("Invalid data.");
            }

            var newOrder = await _orderService.CreateOrderAsync(orderDto);

            return CreatedAtAction(nameof(GetOrderById), new {id = newOrder.OrderId}, newOrder)
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOrder(Guid id)
        {
            var isDeleted = await _orderService.DeleteOrderAsync(id);

            if (!isDeleted)
            {
                return NotFound("Order not found.");
            }

            return NoContent();
        }
    }
}
