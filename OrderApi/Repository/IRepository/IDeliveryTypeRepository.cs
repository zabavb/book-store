using OrderApi.Models;

namespace OrderApi.Repository.IRepository
{
    public interface IDeliveryTypeRepository
    {
        Task<IEnumerable<DeliveryType>> GetAllAsync();
        Task<DeliveryType?> GetByIdAsync(Guid id);
        Task AddAsync(DeliveryType order);
        Task DeleteAsync(DeliveryType order);
        Task UpdateAsync(DeliveryType order);
    }
}
