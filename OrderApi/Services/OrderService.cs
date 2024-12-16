using AutoMapper;
using OrderApi.Models;

namespace OrderApi.Services
{
    internal class OrderService : IOrderService
    {
        private readonly IOrderRepository _repository;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrderDto>> GetOrdersAsync()
        {
            var orders = await _repository.GetAllAsync();

            if (orders == null || !orders.Any())
            {
                return new List<OrderDto>();
            }

            return _mapper.Map<List<OrderDto>>(orders);
        }

        public async Task<OrderDto?> GetOrderByIdAsync(Guid orderId)
        {
            var order = await _repository.GetByIdAsync(orderId);

            if (order == null)
            {
                return null;
            }

            return _mapper.Map<OrderDto>(order);
        }

        public async Task<OrderDto> CreateOrderAsync(OrderDto orderDto)
        {
            var order = _mapper.Map<Order>(orderDto);

            await _repository.AddAsync(order);

            return _mapper.Map<OrderDto>(order);
        }

        public async Task<bool> DeleteOrderAsync(Guid id)
        {
            var order = await _repository.GetByIdAsync(id);

            if (order == null)
            {
                return false;
            }

            await _repository.DeleteAsync(order);
            return true;
        }

        public async Task<OrderDto?> UpdateOrderAsync(Guid id, OrderDto orderDto)
        {
            var order = await _repository.GetByIdAsync(id);

            if (order == null)
            {
                return null;
            }

            // Map the changes to the existing order
            order.UserId = orderDto.UserId;
            order.BookIds = orderDto.BookIds;
            order.Price = orderDto.Price;
            order.DeliveryPrice = orderDto.DeliveryPrice;
            order.Address = orderDto.Address;
            order.City = orderDto.City;
            order.Region = orderDto.Region;
            order.Delivery = orderDto.Delivery;
            order.DeliveryDate = orderDto.DeliveryDate;
            order.DeliveryTime = orderDto.DeliveryTime;
            order.Status = orderDto.Status;

            await _repository.UpdateAsync(order);

            return _mapper.Map<OrderDto>(order);
        }
    }
}
