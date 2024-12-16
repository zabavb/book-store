using AutoMapper;
using OrderApi.Models;
using OrderApi.Models.Extensions;

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

        public async Task<PaginatedResult<Order>> GetOrdersAsync(int pageNumber, int pageSize)
        {
            IEnumerable<Order> orders;
            orders = await _repository.GetAllAsync();
            if (orders == null || !orders.Any())
            {
                return new PaginatedResult<Order>();
            }

            var totalOrders = await Task.FromResult(orders.Count());

            orders = await Task.FromResult(orders.Skip((pageNumber - 1) * pageSize).Take(pageSize));

            return new PaginatedResult<Order>
            {
                Items = (ICollection<Order>)orders,
                TotalCount = totalOrders,
                PageNumber = pageNumber,
                PageSize = pageSize
            }
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
            order.OrderDate = orderDto.OrderDate;
            order.DeliveryDate = orderDto.DeliveryDate;
            order.Status = orderDto.Status;

            await _repository.UpdateAsync(order);

            return _mapper.Map<OrderDto>(order);
        }
    }
}
