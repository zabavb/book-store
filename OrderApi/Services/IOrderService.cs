using OrderApi.Models;
using OrderApi.Models.Extensions;

namespace OrderApi.Services
{
    internal interface IOrderService
    {
        Task<PaginatedResult<Order>> GetOrdersAsync(int pageNumber, int pageSize);
        Task<OrderDto> GetOrderByIdAsync(Guid orderId);
        Task<OrderDto> CreateOrderAsync(OrderDto orderDto);
        Task<OrderDto> UpdateOrderAsync(Guid id, OrderDto orderDto);
        Task<bool> DeleteOrderAsync(Guid id);
    }
}
