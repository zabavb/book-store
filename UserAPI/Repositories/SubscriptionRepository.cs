using Microsoft.EntityFrameworkCore;
using UserAPI.Data;
using UserAPI.Models;
using UserAPI.Models.Extensions;

namespace UserAPI.Repositories
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        private readonly UserDbContext _context;

        public SubscriptionRepository(UserDbContext context)
        {
            _context = context;
        }

        public async Task<PaginatedResult<Subscription>> GetAllEntitiesPaginatedAsync(int pageNumber, int pageSize, string searchTerm)
        {
            IEnumerable<Subscription> subscriptions;
            if (string.IsNullOrWhiteSpace(searchTerm))
                subscriptions = await SearchEntitiesAsync(searchTerm);
            else
                subscriptions = _context.Subscriptions.AsNoTracking();

            var totalSubscriptions = await Task.FromResult(subscriptions.Count());

            subscriptions = await Task.FromResult(subscriptions.Skip((pageNumber - 1) * pageSize).Take(pageSize));

            if (subscriptions == null)
                throw new InvalidOperationException("Failed to fetch subscriptions.");

            return new PaginatedResult<Subscription>
            {
                Items = (ICollection<Subscription>)subscriptions,
                TotalCount = totalSubscriptions,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task<Subscription?> GetEntityByIdAsync(Guid id)
        {
            var subscription = await _context.Subscriptions.AsNoTracking().FirstOrDefaultAsync(s => s.SubscriptionId == id);

            if (subscription == null)
                throw new KeyNotFoundException($"Subscription with ID {id} not found.");

            return subscription;
        }

        public async Task<IEnumerable<Subscription>> SearchEntitiesAsync(string searchTerm)
        {
            var subscriptions = await _context.Subscriptions
                .AsNoTracking()
                .Where(s => s.Title.Contains(searchTerm) || s.Description!.Contains(searchTerm))
                .ToListAsync();

            if (subscriptions == null)
                throw new InvalidOperationException("Failed to search subscriptions.");

            return subscriptions;
        }

        public async Task AddEntityAsync(Subscription entity)
        {
            if (entity == null)
                throw new ArgumentNullException("Subscription was not found.", nameof(entity));

            try
            {
                await _context.Subscriptions.AddAsync(entity);
                await _context.SaveChangesAsync();
            }
            catch (ArgumentNullException ex)
            {
                throw new ArgumentException("Subscription entity cannot be null.", ex);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error occurred while adding the subscription to the database.", ex);
            }
        }

        public async Task UpdateEntityAsync(Subscription entity)
        {
            if (entity == null)
                throw new ArgumentNullException("Subscription was not found.", nameof(entity));

            if (!await _context.Subscriptions.AnyAsync(s => s.SubscriptionId == entity.SubscriptionId))
                throw new InvalidOperationException($"Subscription with ID {entity.SubscriptionId} does not exist.");

            _context.Subscriptions.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteEntityAsync(Guid id)
        {
            var subscription = await _context.Subscriptions.FindAsync(id);

            if (subscription == null)
                throw new KeyNotFoundException($"User with ID {id} not found for deletion.");

            _context.Subscriptions.Remove(subscription);
            await _context.SaveChangesAsync();
        }
    }
}
