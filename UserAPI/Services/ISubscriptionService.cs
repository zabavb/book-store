using UserAPI.Models.DTOs;
using UserAPI.Models;

namespace UserAPI.Services
{
    public interface ISubscriptionService
    {
        Task<PaginatedResult<SubscriptionDTO>> GetAllEntitiesPaginatedAsync(int pageNumber, int pageSize, string searchTerm);
        Task<SubscriptionDTO?> GetEntityByIdAsync(Guid id);
        Task AddEntityAsync(SubscriptionDTO entity);
        Task UpdateEntityAsync(SubscriptionDTO entity);
        Task DeleteEntityAsync(Guid id);
    }
}
