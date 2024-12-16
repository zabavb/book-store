using Library.UserEntities;

namespace UserAPI.Services.PasswordService
{
    public interface IPasswordService
    {
        Task<bool> VerifyPasswordAsync(Guid userId, string plainPassword);
        Task CreatePasswordAsync(UserDTO user, string password);
        Task UpdatePasswordAsync(Guid userId, string newPassword);
        Task<Password> GetByUserIdAsync(Guid userId);
        
    }
}
