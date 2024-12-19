using AutoMapper;
using Library.UserEntities;
using Microsoft.EntityFrameworkCore;
using UserAPI.Data;

namespace UserAPI.Repository
{
    public class PasswordRepository : IPasswordRepository
    {

        private readonly UserDbContext _context;

        public PasswordRepository(UserDbContext context)
        {
            _context = context;

        }
        public async Task AddAsync(Password password)
        {
            await _context.Passwords.AddAsync(password);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Password password)
        {
            _context.Passwords.Remove(password);
            await _context.SaveChangesAsync();

        }

        public async Task<Password> GetByUserIdAsync(Guid userId)
        {
            return await _context.Passwords.FirstOrDefaultAsync(p => p.UserId == userId);
        }

        public async Task UpdateAsync(Password password)
        {
            _context.Passwords.Update(password);
            await _context.SaveChangesAsync();
        }
    }
}
