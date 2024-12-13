using AutoMapper;
using UserAPI.Abstractions;
using UserAPI.Models;
using UserAPI.Models.DTOs;
using UserAPI.Repositories;

namespace UserAPI.Services
{
    public class UserService : IManager<UserDTO>
    {
        private readonly UserRepository _repository;
        private readonly IMapper _mapper;

        public UserService(UserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<UserDTO>> GetAllEntitiesPaginatedAsync(int pageNumber, int pageSize)
        {
            var paginatedUsers = await _repository.GetAllEntitiesPaginatedAsync(pageNumber, pageSize);

            if (paginatedUsers == null || paginatedUsers.Items == null)
                throw new InvalidOperationException("Failed to fetch paginated users.");

            return new PaginatedResult<UserDTO>
            {
                Items = _mapper.Map<ICollection<UserDTO>>(paginatedUsers.Items),
                TotalCount = paginatedUsers.TotalCount,
                PageNumber = paginatedUsers.PageNumber,
                PageSize = paginatedUsers.PageSize
            };
        }

        public async Task<UserDTO?> GetEntityByIdAsync(Guid id)
        {
            var user = await _repository.GetEntityByIdAsync(id);
            
            if (user == null)
                throw new KeyNotFoundException($"User with ID {id} not found.");

            return user == null ? null : _mapper.Map<UserDTO>(user);
        }

        public async Task<ICollection<UserDTO>> SearchEntitiesAsync(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                throw new ArgumentException("Search term cannot be null or whitespace.", nameof(searchTerm));

            var users = await _repository.SearchEntitiesAsync(searchTerm);
            return _mapper.Map<ICollection<UserDTO>>(users);
        }

        public async Task AddEntityAsync(UserDTO entity)
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

        public async Task UpdateEntityAsync(UserDTO entity)
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
