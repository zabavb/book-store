using Microsoft.AspNetCore.Mvc;
using UserAPI.Models.Extensions;
using UserAPI.Services;

namespace UserAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UsersController> _logger;
        private string _message;

        public UsersController(IUserService userService, ILogger<UsersController> logger, string message)
        {
            _userService = userService;
            _logger = logger;
            _message = message;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string? searchTerm = null, [FromQuery] UserFilter? filter = null)
        {
            try
            {
                var users = await _userService.GetAllEntitiesPaginatedAsync(pageNumber, pageSize, searchTerm!, filter);
                _logger.LogInformation("Users successfully fetched.");
                return Ok(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            try
            {
                var user = await _userService.GetEntityByIdAsync(id);
                if (user == null)
                {
                    _message = $"User with ID {id} was not provided.";
                    _logger.LogError(_message);
                    return NotFound(new { message = _message });
                }

                _logger.LogInformation($"User with ID [{id}] successfully fetched.");
                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserDto user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _userService.AddEntityAsync(user);
                _logger.LogInformation($"User with ID [{user.Id}] successfully created.");
                return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UserDto user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != user.Id)
            {
                _message = "User ID in the URL does not match the ID in the body.";
                _logger.LogError(_message);
                return BadRequest(new { message = _message });
            }

            try
            {
                await _userService.UpdateEntityAsync(user);
                _logger.LogInformation($"User with ID [{user.Id}] successfully updated.");
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogError(ex.Message);
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            try
            {
                await _userService.DeleteEntityAsync(id);
                _logger.LogInformation($"User with ID [{id}] successfully deleted.");
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogError(ex.Message);
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
