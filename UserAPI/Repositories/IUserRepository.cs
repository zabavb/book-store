using UserAPI.Models;
using UserAPI.Models.Extensions;

namespace UserAPI.Repositories
{
    public interface IUserRepository
    {
        Task<PaginatedResult<User>> GetAllAsync(int pageNumber, int pageSize, string searchTerm, Filter? filter);
        Task<User?> GetByIdAsync(Guid id);
        Task<IEnumerable<User>> SearchAsync(string searchTerm);
        Task<IEnumerable<User>> FilterAsync(IEnumerable<User> users, Filter filter);
        Task AddAsync(User entity);
        Task UpdateAsync(User entity);
        Task DeleteAsync(Guid id);
    }
}