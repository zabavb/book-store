using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OrderApi.Data;
using OrderApi.Models;

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
            var order = await _context.Orders
                .FirstOrDefaultAsync(o => o.Id == orderId);

            if (order == null)
            {
                return null;
            }

            return _mapper.Map<OrderDto>(order);
        }

        public async Task<OrderDto> CreateOrderAsync(OrderDto orderDto)
        {
            var order = _mapper.Map<Order>(orderDto);

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return _mapper.Map<OrderDto>(order);
        }

        public async Task<bool> DeleteOrderAsync(Guid id)
        {
            var order = await _context.Orders
                .FirstOrDefaultAsync(o => o.Id == id);

            if(order == null) { return false; }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<OrderDto> UpdateOrderAsync(Guid id, OrderDto orderDto)
        {
            var order = await _context.Orders
                .FirstOrDefaultAsync(o => o.Id == id);

            if(order == null) { return null; }


            order.UserId = orderDto.UserId;
            order.BookIds = orderDto.BookIds;

            order.Price = orderDto.Price;
            order.DeliveryPrice = orderDto.DeliveryPrice;

            order.Address = orderDto.Address;
            order.City = orderDto.City;
            order.Region = orderDto.Region;

            order.Delivery = (DeliveryType)orderDto.Delivery;
            order.DeliveryDate = orderDto.DeliveryDate;
            order.DeliveryTime = orderDto.DeliveryTime;

            await _context.SaveChangesAsync();

            return _mapper.Map<OrderDto>(order);
        }
    }
}
