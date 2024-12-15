using UserAPI.Repositories;
using AutoMapper;
using UserAPI.Models.Extensions;
using UserAPI.Models;

namespace UserAPI.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly SubscriptionRepository _repository;
        private readonly IMapper _mapper;

        public SubscriptionService(SubscriptionRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<SubscriptionDto>> GetAllEntitiesPaginatedAsync(int pageNumber, int pageSize, string searchTerm)
        {
            var paginatedSubscriptions = await _repository.GetAllEntitiesPaginatedAsync(pageNumber, pageSize, searchTerm);

            if (paginatedSubscriptions == null || paginatedSubscriptions.Items == null)
                throw new InvalidOperationException("Failed to fetch paginated subscriptions.");

            return new PaginatedResult<SubscriptionDto>
            {
                Items = _mapper.Map<ICollection<SubscriptionDto>>(paginatedSubscriptions.Items),
                TotalCount = paginatedSubscriptions.TotalCount,
                PageNumber = paginatedSubscriptions.PageNumber,
                PageSize = paginatedSubscriptions.PageSize
            };
        }

        public async Task<SubscriptionDto?> GetEntityByIdAsync(Guid id)
        {
            var subscription = await _repository.GetEntityByIdAsync(id);

            if (subscription == null)
                throw new KeyNotFoundException($"Subscription with ID {id} not found.");

            return subscription == null ? null : _mapper.Map<SubscriptionDto>(subscription);
        }

        public async Task AddEntityAsync(SubscriptionDto entity)
        {
            if (entity == null)
                throw new ArgumentNullException("Subscription was not found.", nameof(entity));

            var subscription = _mapper.Map<Subscription>(entity);
            try
            {
                await _repository.AddEntityAsync(subscription);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error occurred while adding the subscription.", ex);
            }
        }

        public async Task UpdateEntityAsync(SubscriptionDto entity)
        {
            if (entity == null)
                throw new ArgumentNullException("Subscription was not found.", nameof(entity));

            var subscription = _mapper.Map<Subscription>(entity);
            try
            {
                await _repository.UpdateEntityAsync(subscription);
            }
            catch (InvalidOperationException)
            {
                throw new KeyNotFoundException($"Subscription with ID {entity.Id} not found for update.");
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error occurred while updating the subscription.", ex);
            }
        }

        public async Task DeleteEntityAsync(Guid id)
        {
            try
            {
                await _repository.DeleteEntityAsync(id);
            }
            catch (InvalidOperationException)
            {
                throw new KeyNotFoundException($"Subscription with ID {id} not found for deletion.");
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error occurred while deleting the subscription.", ex);
            }
        }
    }
}
