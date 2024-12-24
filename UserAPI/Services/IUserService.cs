using UserAPI.Models.Extensions;

namespace UserAPI.Services
{
    public interface IUserService
    {
        Task<PaginatedResult<UserDto>> GetAllAsync(int pageNumber, int pageSize, string searchTerm, Filter? filter);
        Task<UserDto?> GetByIdAsync(Guid id);
        Task AddAsync(UserDto entity);
        Task UpdateAsync(UserDto entity);
        Task DeleteAsync(Guid id);
    }
}
