using UserAPI.Models.Extensions;

namespace UserAPI.Services
{
    public interface IUserService
    {
        Task<PaginatedResult<UserDto>> GetAllEntitiesPaginatedAsync(int pageNumber, int pageSize, string searchTerm, UserFilter? filter);
        Task<UserDto?> GetEntityByIdAsync(Guid id);
        Task AddEntityAsync(UserDto entity);
        Task UpdateEntityAsync(UserDto entity);
        Task DeleteEntityAsync(Guid id);
    }
}
