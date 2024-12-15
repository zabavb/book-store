using UserAPI.Models;
using UserAPI.Models.Extensions;

namespace UserAPI.Repositories
{
    public interface ISubscriptionRepository
    {
        Task<PaginatedResult<Subscription>> GetAllEntitiesPaginatedAsync(int pageNumber, int pageSize, string searchTerm);
        Task<Subscription?> GetEntityByIdAsync(Guid id);
        Task<IEnumerable<Subscription>> SearchEntitiesAsync(string searchTerm);
        Task AddEntityAsync(Subscription entity);
        Task UpdateEntityAsync(Subscription entity);
        Task DeleteEntityAsync(Guid id);
    }
}
