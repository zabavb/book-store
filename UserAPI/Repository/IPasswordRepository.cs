using Library.UserEntities;

namespace UserAPI.Repository
{
    public interface IPasswordRepository
    {
        Task<Password> GetByUserIdAsync(Guid userId);
        Task AddAsync(Password password);
        Task UpdateAsync(Password password);
        Task DeleteAsync(Password password);
    }
}
