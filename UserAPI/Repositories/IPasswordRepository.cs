
using OrderApi.Models.Extensions;
using UserAPI.Models;


namespace UserAPI.Repositories
{
    public interface IPasswordRepository
    {
        
        Task<bool> VerifyPasswordAsync(Guid userId, string plainPassword);
        Task<bool> AddPasswordAsync(string password, User user);
        Task<bool> UpdatePasswordAsync(Guid userId, string newPassword);
        Task<bool> DeletePasswordAsync(Guid passwordId);
        Task<Password> GetEntityByPasswordIdAsync(Guid passwordId);
        Task<string> GetHashByPasswordIdAsync(Guid passwordId);
    }
}
