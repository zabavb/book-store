using Library.UserEntities;
using Microsoft.EntityFrameworkCore;
using UserAPI.Data;
using UserAPI.Models;

namespace UserAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDbContext _context;

        public UserRepository(UserDbContext context)
        {
            _context = context;
        }

        public async Task<PaginatedResult<User>> GetAllEntitiesPaginatedAsync(int pageNumber, int pageSize, string searchTerm, UserFilter? filter)
        {
            IEnumerable<User> users;
            if (string.IsNullOrWhiteSpace(searchTerm))
                users = await SearchEntitiesAsync(searchTerm);
            else
                users = _context.Users.AsNoTracking();
            if (users.Any() && filter != null)
                users = await FilterEntitiesAsync(users, filter);

            var totalUsers = await Task.FromResult(users.Count());

            users = await Task.FromResult(users.Skip((pageNumber - 1) * pageSize).Take(pageSize));

            if (users == null)
                throw new InvalidOperationException("Failed to fetch users.");

            return new PaginatedResult<User>
            {
                Items = (ICollection<User>)users,
                TotalCount = totalUsers,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task<User?> GetEntityByIdAsync(Guid id)
        {
            var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.UserId == id);
            
            if (user == null)
                throw new KeyNotFoundException($"User with ID {id} not found.");

            return user;
        }

        public async Task<IEnumerable<User>> SearchEntitiesAsync(string searchTerm)
        {
            var users = await _context.Users
                .AsNoTracking()
                .Where(u => u.FirstName.Contains(searchTerm)
                            || u.LastName!.Contains(searchTerm)
                            || u.Email.Contains(searchTerm))
                .ToListAsync();

            if (users == null)
                throw new InvalidOperationException("Failed to search users.");

            return users;
        }

        public async Task<IEnumerable<User>> FilterEntitiesAsync(IEnumerable<User> users, UserFilter filter)
        {
            if (filter.Role.HasValue)
                users = users.Where(u => u.Role.Equals(filter.Role));

            if (filter.DateOfBirthStart.HasValue)
                users = users.Where(u => u.DateOfBirth >= filter.DateOfBirthStart.Value);

            if (filter.DateOfBirthEnd.HasValue)
                users = users.Where(u => u.DateOfBirth <= filter.DateOfBirthEnd.Value);

            return await Task.FromResult(users);
        }

        public async Task AddEntityAsync(User entity)
        {
            if (entity == null)
                throw new ArgumentNullException("User was not found.", nameof(entity));

            try
            {
                await _context.Users.AddAsync(entity);
                await _context.SaveChangesAsync();
            }
            catch (ArgumentNullException ex)
            {
                throw new ArgumentException("User entity cannot be null.", ex);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error occurred while adding the user to the database.", ex);
            }
        }

        public async Task UpdateEntityAsync(User entity)
        {
            if (entity == null)
                throw new ArgumentNullException("User was not found.", nameof(entity));

            if (!await _context.Users.AnyAsync(u => u.UserId == entity.UserId))
                throw new InvalidOperationException($"User with ID {entity.UserId} does not exist.");

            _context.Users.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteEntityAsync(Guid id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
                throw new KeyNotFoundException($"User with ID {id} not found for deletion.");

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}
