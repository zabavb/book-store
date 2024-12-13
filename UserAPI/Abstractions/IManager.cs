using AutoMapper;
using UserAPI.Models;

namespace UserAPI.Abstractions
{
    public interface IManager<T>
    {
        Task<PaginatedResult<T>> GetAllEntitiesPaginatedAsync(int pageNumber, int pageSize);
        Task<T?> GetEntityByIdAsync(Guid id);
        Task<ICollection<T>> SearchEntitiesAsync(string searchTerm);
        Task AddEntityAsync(T entity);
        Task UpdateEntityAsync(T entity);
        Task DeleteEntityAsync(Guid id);
    }
}
