using OrderApi.Models;

namespace OrderApi.Services
{
    internal interface IOrderService
    {
        Task<IEnumerable<OrderDto>> GetOrdersAsync();
        Task<OrderDto> GetOrderByIdAsync(Guid orderId);
        Task<OrderDto> CreateOrderAsync(Order orderDto);
        Task<OrderDto> UpdateOrderAsync(Guid id, Order orderDto);
        Task<bool> DeleteOrderAsync(Guid id);
    }
}
