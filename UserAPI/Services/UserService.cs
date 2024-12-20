using AutoMapper;
using UserAPI.Models;
using UserAPI.Models.Extensions;
using UserAPI.Repositories;

namespace UserAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<IUserService> _logger;
        private string _message;

        public UserService(IUserRepository repository, IMapper mapper, ILogger<IUserService> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _message = string.Empty;
        }

        public async Task<PaginatedResult<UserDto>> GetAllEntitiesPaginatedAsync(int pageNumber, int pageSize, string searchTerm, UserFilter? filter)
        {
            var paginatedUsers = await _repository.GetAllEntitiesPaginatedAsync(pageNumber, pageSize, searchTerm, filter);

            if (paginatedUsers == null || paginatedUsers.Items == null)
            {
                _message = "Failed to fetch paginated users.";
                _logger.LogError(_message);
                throw new InvalidOperationException(_message);
            }
            _logger.LogInformation("Successfully fetched [{Count}] users.", paginatedUsers.Items.Count());

            return new PaginatedResult<UserDto>
            {
                Items = _mapper.Map<ICollection<UserDto>>(paginatedUsers.Items),
                TotalCount = paginatedUsers.TotalCount,
                PageNumber = paginatedUsers.PageNumber,
                PageSize = paginatedUsers.PageSize
            };
        }

        public async Task<UserDto?> GetEntityByIdAsync(Guid id)
        {
            var user = await _repository.GetEntityByIdAsync(id);
            
            if (user == null)
            {
                _message = $"User with ID [{id}] not found.";
                _logger.LogError(_message);
                throw new KeyNotFoundException(_message);
            }
            _logger.LogInformation($"User with ID [{id}] successfully fetched.");
            
            return user == null ? null : _mapper.Map<UserDto>(user);
        }

        public async Task AddEntityAsync(UserDto entity)
        {
            if (entity == null)
            {
                _message = "User was not provided for creation.";
                _logger.LogError(_message);
                throw new ArgumentNullException(_message, nameof(entity));
            }

            var user = _mapper.Map<User>(entity);
            try
            {
                await _repository.AddEntityAsync(user);
                _logger.LogInformation("User successfully created.");
            }
            catch (Exception ex)
            {
                _message = $"Error occurred while adding the user with ID [{entity.Id}].";
                _logger.LogError(_message);
                throw new InvalidOperationException(_message, ex);
            }
        }

        public async Task UpdateEntityAsync(UserDto entity)
        {
            if (entity == null)
            {
                _message = "User was not provided for update.";
                _logger.LogError(_message);
                throw new ArgumentNullException(_message, nameof(entity));
            }

            var user = _mapper.Map<User>(entity);
            try
            {
                await _repository.UpdateEntityAsync(user);
                _logger.LogInformation($"User with ID [{entity.Id}] successfully updated.");
            }
            catch (InvalidOperationException)
            {
                _message = $"User with ID {entity.Id} not found for update.";
                _logger.LogError(_message);
                throw new KeyNotFoundException(_message);
            }
            catch (Exception ex)
            {
                _message = $"Error occurred while updating the user with ID [{entity.Id}].";
                _logger.LogError(_message);
                throw new InvalidOperationException(_message, ex);
            }
        }

        public async Task DeleteEntityAsync(Guid id)
        {
            try
            {
                await _repository.DeleteEntityAsync(id);
                _logger.LogError($"User with ID [{id}] successfully deleted."); 
            }
            catch (KeyNotFoundException)
            {
                _message = $"User with ID [{id}] not found for deletion.";
                _logger.LogError(_message);
                throw new KeyNotFoundException(_message);
            }
            catch (Exception ex)
            {
                _message = $"Error occurred while deleting the user with ID [{id}].";
                _logger.LogError(_message);
                throw new InvalidOperationException(_message, ex);
            }
        }
    }
}
