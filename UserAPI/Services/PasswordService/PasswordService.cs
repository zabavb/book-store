using Library.UserEntities;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using UserAPI.Data;

namespace UserAPI.Services.PasswordService
{
    public class PasswordService : IPasswordService
    {

        private readonly UserDbContext _context;

        public PasswordService(UserDbContext context)
        {
            _context = context;

        }

        public async Task CreatePasswordAsync(UserDTO user, string password)
        {
            var salt = GenerateSalt();
            var hash = HashPassword(password, salt);

            var passwordEntity = new Password
            {
                PasswordId = Guid.NewGuid(),
                PasswordHash = hash,
                PasswordSalt = salt,
                UserId = user.UserId
            };

            await _context.AddAsync(passwordEntity);
        }

        private string HashPassword(string password, string salt)
        {
            using var sha256 = SHA256.Create();
            var combined = Encoding.UTF8.GetBytes(password + salt);
            var hash = sha256.ComputeHash(combined);
            var result = Convert.ToBase64String(hash);
            return result.Substring(0, result.Length - 1);
        }

        // size -> size % 8 == 0
        private string GenerateSalt(int size = 8)
        {
            if (size % 8 != 0)
            {
                Console.WriteLine("Wrong size of salt");
                return null;
            }

            var saltBytes = new byte[size];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(saltBytes);
            }
            string result = Convert.ToBase64String(saltBytes);
            return result.Substring(0, result.Length - (size/8));
        }

        public async Task<Password> GetByUserIdAsync(Guid userId)
        {
            return await _context.Passwords.FirstOrDefaultAsync(p => p.UserId == userId);
        }

        public Task UpdatePasswordAsync(Guid userId, string newPassword)
        {
            throw new NotImplementedException();
        }

        public Task<bool> VerifyPasswordAsync(Guid userId, string plainPassword)
        {
            throw new NotImplementedException();
        }


    }
}
