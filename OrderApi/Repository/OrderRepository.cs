using Microsoft.EntityFrameworkCore;
using OrderApi.Data;
using OrderApi.Models;
using OrderApi.Services;

namespace OrderApi.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderDbContext _context;
        private readonly ILogger<IOrderRepository> _logger;
        private string _message;
        public OrderRepository(OrderDbContext context, ILogger<IOrderRepository> logger)
        {
            _context = context;
            _logger = logger;
            _message = string.Empty;
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            var orders = await _context.Orders.ToListAsync();
            if (orders == null)
            {
                _message = "Failed to fetch orders.";
                _logger.LogError(_message);
                throw new InvalidOperationException(_message);
            }
            else
                _logger.LogInformation("Successfully fetched orders");

            return orders;
        }

        public async Task<Order?> GetByIdAsync(Guid id)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.OrderId == id);
            if (order == null)
            {
                _message = $"Order with Id [{id}] not found.";
                _logger.LogError(_message);
                throw new KeyNotFoundException(_message);
            }
            else
                _logger.LogInformation($"Order with Id [{id}] found.");
            return order == null ? null : order;
        }

        public async Task AddAsync(Order order)
        {
            if (order == null)
            {
                _message = "Order was not provided for creation";
                _logger.LogError (_message);
                throw new ArgumentNullException(_message, nameof(order));
            }
            try
            {
                await _context.Orders.AddAsync(order);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Order created succesfully");
            }
            catch (ArgumentNullException ex)
            {
                _message = "Order entity cannot be null.";
                _logger.LogError(_message);
                throw new ArgumentException(_message, ex);
            }
            catch (Exception ex) {
                _message = "Error occured while adding the order to database.";
                _logger.LogError(_message);
                throw new InvalidOperationException(_message, ex);
            }
        }

        public async Task UpdateAsync(Order order)
        {
            if (order == null)
            {
                _message = "Order was not provided for update.";
                _logger.LogError (_message);
                throw new ArgumentNullException (_message, nameof(order));
            }

            if(!await _context.Orders.AnyAsync(o => o.OrderId == order.OrderId))
            {
                _message = $"Order with Id [{order.OrderId}] does not exist.";
                _logger.LogError (_message);
                throw new InvalidOperationException(_message);
            }

            _context.Orders.Update(order);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"Order with Id[{order.OrderId}] updated succesfully.");
        }

        public async Task DeleteAsync(Order order)
        {
            try
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _message = $"Deletion of Order with id [{order.OrderId}] has failed.";
                _logger.LogError(_message);
                throw new ArgumentException(_message, ex);
            }

            _logger.LogInformation($"Order with Id [{order.OrderId}] deleted succesfully.");
        }
    }

}
