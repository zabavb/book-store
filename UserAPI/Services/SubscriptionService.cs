using UserAPI.Repositories;
using AutoMapper;
using UserAPI.Models.Extensions;
using UserAPI.Models;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace UserAPI.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly SubscriptionRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<ISubscriptionService> _logger;
        private string _message;

        public SubscriptionService(SubscriptionRepository repository, IMapper mapper, ILogger<ISubscriptionService> logger, string message)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _message = message;
        }

        public async Task<PaginatedResult<SubscriptionDto>> GetAllEntitiesPaginatedAsync(int pageNumber, int pageSize, string searchTerm)
        {
            var paginatedSubscriptions = await _repository.GetAllEntitiesPaginatedAsync(pageNumber, pageSize, searchTerm);

            if (paginatedSubscriptions == null || paginatedSubscriptions.Items == null)
            {
                _message = "Failed to fetch paginated subscriptions.";
                _logger.LogError(_message);
                throw new InvalidOperationException(_message);
            }

            _logger.LogInformation("Subscriptions successfully fetched.");

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
            {
                _message = $"Subscription with ID [{id}] not found.";
                _logger.LogError(_message);
                throw new KeyNotFoundException(_message);
            }

            _logger.LogInformation($"Subscription with ID [{id}] successfully fetched.");
            return subscription == null ? null : _mapper.Map<SubscriptionDto>(subscription);
        }

        public async Task AddEntityAsync(SubscriptionDto entity)
        {
            if (entity == null)
            {
                _message = "Subscription was not provided for creation.";
                _logger.LogError(_message);
                throw new ArgumentNullException(_message, nameof(entity));
            }

            var subscription = _mapper.Map<Subscription>(entity);
            try
            {
                await _repository.AddEntityAsync(subscription);
                _logger.LogInformation($"Subscription with ID [{entity.Id}] successfully created.");
            }
            catch (Exception ex)
            {
                _message = $"Error occurred while adding the subscription with ID [{entity.Id}].";
                _logger.LogError(_message);
                throw new InvalidOperationException(_message, ex);
            }
        }

        public async Task UpdateEntityAsync(SubscriptionDto entity)
        {
            if (entity == null)
            {
                _message = "Subscription was not provided for update.";
                _logger.LogError(_message);
                throw new ArgumentNullException(_message, nameof(entity));
            }

            var subscription = _mapper.Map<Subscription>(entity);
            try
            {
                await _repository.UpdateEntityAsync(subscription);
                _logger.LogInformation($"Subscription successfully updated to [{entity}].");
            }
            catch (InvalidOperationException)
            {
                _message = $"Subscription with ID {entity.Id} not found for update.";
                _logger.LogError(_message);
                throw new KeyNotFoundException(_message);
            }
            catch (Exception ex)
            {
                _message = $"Error occurred while updating the subscription with ID [{entity.Id}].";
                _logger.LogError(_message);
                throw new InvalidOperationException(_message, ex);
            }
        }

        public async Task DeleteEntityAsync(Guid id)
        {
            try
            {
                await _repository.DeleteEntityAsync(id);
                _logger.LogInformation($"Subscription with ID [{id}] successfully deleted.");
            }
            catch (InvalidOperationException)
            {
                _message = $"Subscription with ID {id} not found for deletion.";
                _logger.LogError(_message);
                throw new KeyNotFoundException(_message);
            }
            catch (Exception ex)
            {
                _message = $"Error occurred while deleting the subscription with ID [{id}].";
                _logger.LogError(_message);
                throw new InvalidOperationException(_message, ex);
            }
        }
    }
}
