using UserAPI.Models;
using UserAPI.Models.Extensions;

namespace UserAPI.Repositories
{
    public interface ISubscriptionRepository
    {
        Task<PaginatedResult<Subscription>> GetAllAsync(int pageNumber, int pageSize, string searchTerm);
        Task<Subscription?> GetByIdAsync(Guid id);
        Task<IEnumerable<Subscription>> SearchAsync(string searchTerm);
        Task AddAsync(Subscription entity);
        Task UpdateAsync(Subscription entity);
        Task DeleteAsync(Guid id);
    }
}
