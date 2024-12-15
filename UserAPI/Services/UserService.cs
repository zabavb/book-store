using AutoMapper;
using UserAPI.Models;
using UserAPI.Models.Extensions;
using UserAPI.Repositories;

namespace UserAPI.Services
{
    public class UserService : IUserService
    {
        private readonly UserRepository _repository;
        private readonly IMapper _mapper;

        public UserService(UserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<UserDto>> GetAllEntitiesPaginatedAsync(int pageNumber, int pageSize, string searchTerm, UserFilter? filter)
        {
            var paginatedUsers = await _repository.GetAllEntitiesPaginatedAsync(pageNumber, pageSize, searchTerm, filter);

            if (paginatedUsers == null || paginatedUsers.Items == null)
                throw new InvalidOperationException("Failed to fetch paginated users.");

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
                throw new KeyNotFoundException($"User with ID {id} not found.");

            return user == null ? null : _mapper.Map<UserDto>(user);
        }

        public async Task AddEntityAsync(UserDto entity)
        {
            if (entity == null)
                throw new ArgumentNullException("User was not found.", nameof(entity));

            var user = _mapper.Map<User>(entity);
            try
            {
                await _repository.AddEntityAsync(user);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error occurred while adding the user.", ex);
            }
        }

        public async Task UpdateEntityAsync(UserDto entity)
        {
            if (entity == null)
                throw new ArgumentNullException("User was not found.", nameof(entity));

            var user = _mapper.Map<User>(entity);
            try
            {
                await _repository.UpdateEntityAsync(user);
            }
            catch (InvalidOperationException)
            {
                throw new KeyNotFoundException($"User with ID {entity.Id} not found for update.");
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error occurred while updating the user.", ex);
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
                throw new KeyNotFoundException($"User with ID {id} not found for deletion.");
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error occurred while deleting the user.", ex);
            }
        }
    }
}
