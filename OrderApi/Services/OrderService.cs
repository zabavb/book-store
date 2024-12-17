using AutoMapper;
using OrderApi.Models;
using OrderApi.Models.Extensions;

namespace OrderApi.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<IOrderService> _logger;
        private string _message;

        public OrderService(IOrderRepository repository, IMapper mapper, ILogger<IOrderService> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _message = string.Empty;
        }

        public async Task<PaginatedResult<Order>> GetOrdersAsync(int pageNumber, int pageSize)
        {
            IEnumerable<Order> orders;
            orders = await _repository.GetAllAsync();
            if (orders == null || !orders.Any())
            {
                _message = "Failed to fetch orders.";
                _logger.LogError(_message);
                throw new InvalidOperationException(_message);
            }

            _logger.LogInformation("Orders fetched succesfully");

            var totalOrders = await Task.FromResult(orders.Count());

            orders = await Task.FromResult(orders.Skip((pageNumber - 1) * pageSize).Take(pageSize));

            return new PaginatedResult<Order>
            {
                Items = (ICollection<Order>)orders,
                TotalCount = totalOrders,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task<OrderDto?> GetOrderByIdAsync(Guid orderId)
        {
            var order = await _repository.GetByIdAsync(orderId);

            if (order == null)
            {
                _message = $"Order with Id [{orderId}] not found.";
                _logger.LogError(_message);
                throw new KeyNotFoundException(_message);
            }

            _logger.LogInformation($"Order with Id [{orderId}] fetched succesfully.");

            return order == null ? null : _mapper.Map<OrderDto>(order);
        }

        public async Task<OrderDto> CreateOrderAsync(OrderDto orderDto)
        {
            if(orderDto == null)
            {
                _message = "Order wasn't provided.";
                _logger.LogError(_message);
                throw new ArgumentNullException(_message,nameof(orderDto));
            }
            var order = _mapper.Map<Order>(orderDto);

            try
            {
                await _repository.AddAsync(order);
                _logger.LogInformation("Order created successfully.");
            }
            catch(Exception ex)
            {
                _message = "Error occured while adding an order.";
                _logger.LogError(_message);
                throw new InvalidOperationException(_message, ex);
            }

            return _mapper.Map<OrderDto>(order);
        }

        public async Task<OrderDto?> UpdateOrderAsync(OrderDto orderDto)
        {
            if (orderDto == null)
            {
                _message = "Order was not provided for the update";
                _logger.LogError(_message);
                throw new ArgumentNullException(_message,nameof(orderDto));
            }

            var order = _mapper.Map<Order>(orderDto);
            try
            {
                await _repository.UpdateAsync(order);
                _logger.LogInformation("Order updated succesfully.");
            }
            catch (InvalidOperationException)
            {
                _message = $"User with Id {orderDto.Id} not found for update.";
                _logger.LogError(_message);
                throw new KeyNotFoundException(_message);
            }
            catch(Exception ex)
            {
                _message = $"Error occured while updating the order with Id [{orderDto.Id}]";
                _logger.LogError(_message);
                throw new InvalidOperationException(_message, ex);
            }

            return _mapper.Map<OrderDto>(order);
        }

        public async Task<bool> DeleteOrderAsync(Guid id)
        {
            var order = await _repository.GetByIdAsync(id);

            if (order == null)
            {
                _message = $"Order with Id [{id}] not found.";
                _logger.LogError(_message);
                throw new KeyNotFoundException(_message);
            }

            try
            {
                await _repository.DeleteAsync(order);
                _logger.LogInformation($"Order with Id [{id}] deleted succesfully");
            }
            catch (InvalidOperationException)
            {
                _message = $"Order with Id [{id}] not found for deletion.";
                _logger.LogError(_message);
                throw new KeyNotFoundException(_message);
            }
            catch(Exception ex)
            {
                _message = $"Error occurred while deleting the order with Id [{id}].";
                _logger.LogError(_message);
                throw new InvalidOperationException(_message, ex);
            }
            return true;
        }
    }
}
