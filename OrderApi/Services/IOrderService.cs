using OrderApi.Models;
using OrderApi.Models.Extensions;

namespace OrderApi.Services
{
    public interface IOrderService
    {
        Task<PaginatedResult<OrderDto>> GetOrdersAsync(int pageNumber, int pageSize);
        Task<OrderDto> GetOrderByIdAsync(Guid orderId);
        Task<OrderDto> CreateOrderAsync(OrderDto orderDto);
        Task<OrderDto> UpdateOrderAsync(OrderDto orderDto);
        Task<bool> DeleteOrderAsync(Guid id);
    }
}
