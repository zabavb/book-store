using Microsoft.EntityFrameworkCore;
using UserAPI.Data;
using UserAPI.Models;
using UserAPI.Models.Extensions;

namespace UserAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDbContext _context;

        public UserRepository(UserDbContext context) => _context = context;

        public async Task<PaginatedResult<User>> GetAllAsync(int pageNumber, int pageSize, string searchTerm, Filter? filter)
        {
            IEnumerable<User> users;
            if (!string.IsNullOrWhiteSpace(searchTerm))
                users = await SearchAsync(searchTerm);
            else
                users = _context.Users.AsNoTracking();
            if (users.Any() && filter != null)
                users = await FilterAsync(users, filter);

            var totalUsers = await Task.FromResult(users.Count());

            users = await Task.FromResult(users.Skip((pageNumber - 1) * pageSize).Take(pageSize));
            ICollection<User> result = new List<User>(users);
            return new PaginatedResult<User>
            {
                Items = result,
                TotalCount = totalUsers,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task<User?> GetByIdAsync(Guid id) =>
            await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.UserId == id);

        public async Task<IEnumerable<User>> SearchAsync(string searchTerm)
        {
            var users = await _context.Users
                .AsNoTracking()
                .Where(u => u.FirstName.Contains(searchTerm)
                            || u.LastName!.Contains(searchTerm)
                            || u.Email.Contains(searchTerm))
                .ToListAsync();

            return users;
        }

        public async Task<IEnumerable<User>> FilterAsync(IEnumerable<User> users, Filter filter)
        {
            if (filter.Role.HasValue)
                users = users.Where(u => u.Role.Equals(filter.Role));

            if (filter.DateOfBirthStart.HasValue)
                users = users.Where(u => u.DateOfBirth >= filter.DateOfBirthStart.Value);

            if (filter.DateOfBirthEnd.HasValue)
                users = users.Where(u => u.DateOfBirth <= filter.DateOfBirthEnd.Value);

            if (filter.HasSubscription)
                users = users.Where(u => u.SubscriptionId.Equals(filter.HasSubscription));

            return await Task.FromResult(users);
        }

        public async Task AddAsync(User entity)
        {
            await _context.Users.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User entity)
        {
            if (!await _context.Users.AnyAsync(u => u.UserId == entity.UserId))
                throw new InvalidOperationException();

            _context.Users.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
                throw new KeyNotFoundException();

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}
