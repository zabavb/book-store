using UserAPI.Models;
using UserAPI.Models.Extensions;

namespace UserAPI.Repositories
{
    public interface IUserRepository
    {
        Task<PaginatedResult<User>> GetAllEntitiesPaginatedAsync(int pageNumber, int pageSize, string searchTerm, Filter? filter);
        Task<User?> GetEntityByIdAsync(Guid id);
        Task<IEnumerable<User>> SearchEntitiesAsync(string searchTerm);
        Task<IEnumerable<User>> FilterEntitiesAsync(IEnumerable<User> users, Filter filter);
        Task AddEntityAsync(User entity);
        Task UpdateEntityAsync(User entity);
        Task DeleteEntityAsync(Guid id);
    }
}
