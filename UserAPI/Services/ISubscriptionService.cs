using UserAPI.Models.Extensions;

namespace UserAPI.Services
{
    public interface ISubscriptionService
    {
        Task<PaginatedResult<SubscriptionDto>> GetAllEntitiesPaginatedAsync(int pageNumber, int pageSize, string searchTerm);
        Task<SubscriptionDto?> GetEntityByIdAsync(Guid id);
        Task AddEntityAsync(SubscriptionDto entity);
        Task UpdateEntityAsync(SubscriptionDto entity);
        Task DeleteEntityAsync(Guid id);
    }
}
