using UserAPI.Models.DTOs;
using UserAPI.Models;

namespace UserAPI.Services
{
    public interface IUserService
    {
        Task<PaginatedResult<UserDTO>> GetAllEntitiesPaginatedAsync(int pageNumber, int pageSize, string searchTerm, UserFilter? filter);
        Task<UserDTO?> GetEntityByIdAsync(Guid id);
        Task AddEntityAsync(UserDTO entity);
        Task UpdateEntityAsync(UserDTO entity);
        Task DeleteEntityAsync(Guid id);
    }
}
