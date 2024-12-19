using Microsoft.EntityFrameworkCore;
using System.Text;
using UserAPI.Data;
using UserAPI.Models;
using UserAPI.Models.Extensions;

namespace UserAPI.Repositories
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        private readonly UserDbContext _context;
        private readonly ILogger<ISubscriptionRepository> _logger;
        private string _message;

        public SubscriptionRepository(UserDbContext context, ILogger<ISubscriptionRepository> logger)
        {
            _context = context;
            _logger = logger;
            _message = string.Empty;
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
            {
                _message = "Failed to fetch subscriptions.";
                _logger.LogError(_message);
                throw new InvalidOperationException(_message);
            }
            else
                _logger.LogInformation("Subscriptions successfully fetched.");

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
            {
                _message = $"Subscription with ID [{id}] not found.";
                _logger.LogError(_message);
                throw new KeyNotFoundException(_message);
            }
            else
                _logger.LogInformation($"Found subscription with ID [{subscription.SubscriptionId}].");

            return subscription;
        }

        public async Task<IEnumerable<Subscription>> SearchEntitiesAsync(string searchTerm)
        {
            var subscriptions = await _context.Subscriptions
                .AsNoTracking()
                .Where(s => s.Title.Contains(searchTerm) || s.Description!.Contains(searchTerm))
                .ToListAsync();

            if (subscriptions == null)
            {
                _message = "Failed to search subscriptions.";
                _logger.LogError(_message);
                throw new InvalidOperationException(_message);
            }
            else
                _logger.LogInformation("Successfully searched subscriptions.");

            return subscriptions;
        }

        public async Task AddEntityAsync(Subscription entity)
        {
            if (entity == null)
            {
                _message = "Subscription not provided for creation.";
                _logger.LogError(_message);
                throw new ArgumentNullException(_message, nameof(entity));
            }

            try
            {
                await _context.Subscriptions.AddAsync(entity);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Subscription [{entity}] successfully created.");
            }
            catch (ArgumentNullException ex)
            {
                _message = "Subscription cannot be null.";
                _logger.LogError(_message);
                throw new ArgumentException(_message, ex);
            }
            catch (Exception ex)
            {
                _message = "Error occurred while adding the subscription to the database.";
                _logger.LogError(_message);
                throw new InvalidOperationException(_message, ex);
            }
        }

        public async Task UpdateEntityAsync(Subscription entity)
        {
            if (entity == null)
            {
                _message = "Subscription was not provided for update.";
                _logger.LogError(_message);
                throw new ArgumentNullException(_message, nameof(entity));
            }

            if (!await _context.Subscriptions.AnyAsync(s => s.SubscriptionId == entity.SubscriptionId))
            {
                _message = $"Subscription with ID {entity.SubscriptionId} does not exist.";
                _logger.LogError(_message);
                throw new InvalidOperationException(_message);
            }

            _context.Subscriptions.Update(entity);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Subscription successsfully upated to [{entity}].");
        }

        public async Task DeleteEntityAsync(Guid id)
        {
            var subscription = await _context.Subscriptions.FindAsync(id);

            if (subscription == null)
            {
                _message = $"Subscription with ID {id} not found for deletion.";
                _logger.LogError(_message);
                throw new KeyNotFoundException(_message);
            }

            _context.Subscriptions.Remove(subscription);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Subscription with ID [{subscription.SubscriptionId}] successfully deleted.");
        }
    }
}
