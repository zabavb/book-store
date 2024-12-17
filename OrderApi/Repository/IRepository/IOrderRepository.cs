using OrderApi.Models;

public interface IOrderRepository
{
    Task<IEnumerable<Order>> GetAllAsync();
    Task<Order?> GetByIdAsync(Guid id);
    Task AddAsync(Order order);
    Task DeleteAsync(Order order);
    Task UpdateAsync(Order order);
}