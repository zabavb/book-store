using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OrderApi.Data;

namespace OrderApi.Services
{
    public class OrderService : IOrderService
    {
        private readonly OrderDbContext _context;
        private readonly IMapper _mapper;

        public OrderService(OrderDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrderDto>> GetOrdersAsync()
        {
            var orders = await _context.Orders.ToListAsync();

            if (orders == null || orders.Count == 0)
            {
                return [];
            }

            return _mapper.Map<List<OrderDto>>(orders);
        }
        public async Task<OrderDto> GetOrderByIdAsync(Guid orderId)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == orderId);

            if (order == null)
            {
                return null;
            }

            return _mapper.Map<OrderDto>(order);
        }

        public async Task<OrderDto> CreateOrderAsync(OrderDto orderDto)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteOrderAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<OrderDto> UpdateOrderAsync(Guid id, OrderDto orderDto)
        {
            throw new NotImplementedException();
        }
    }
}
