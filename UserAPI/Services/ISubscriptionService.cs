using UserAPI.Models.Extensions;

namespace UserAPI.Services
{
    public interface ISubscriptionService
    {
        Task<PaginatedResult<SubscriptionDto>> GetAllAsync(int pageNumber, int pageSize, string searchTerm);
        Task<SubscriptionDto?> GetByIdAsync(Guid id);
        Task AddAsync(SubscriptionDto entity);
        Task UpdateAsync(SubscriptionDto entity);
        Task DeleteAsync(Guid id);
    }
}
