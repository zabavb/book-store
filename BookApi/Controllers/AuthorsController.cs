using BookApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _authorService;
        private const int DefaultPageNumber = 1;
        private const int DefaultPageSize = 10;
        public AuthorsController(IAuthorService authorService)
        {
            _authorService = authorService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorDto>>> GetAuthors(int pageNumber = DefaultPageNumber, int pageSize = DefaultPageSize)
        {
            if (pageNumber < 1 || pageSize < 1)
            {
                return BadRequest("Page number and page size must be greater than 0.");
            }

            var authors = await _authorService.GetAuthorsAsync();

            if (authors == null || !authors.Any())
            {
                return NotFound("No authors found.");
            }

            var paginated = authors
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return Ok(paginated);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorDto>> GetAuthorById(Guid id)
        {
            var author = await _authorService.GetAuthorByIdAsync(id);

            if (author == null)
            {
                return NotFound($"Author with id {id} not found.");
            }

            return Ok(author);
        }
        [HttpPost]
        public async Task<ActionResult<AuthorDto>> CreateAuthor([FromBody] AuthorDto authorDto)
        {
            if (authorDto == null)
            {
                return BadRequest("Invalid data.");
            }

            var created = await _authorService.CreateAuthorAsync(authorDto);

            return CreatedAtAction(nameof(GetAuthorById), new { id = created.AuthorId }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<AuthorDto>> UpdateAuthor(Guid id, [FromBody] AuthorDto authorDto)
        {
            if (authorDto == null)
            {
                return BadRequest("Invalid data.");
            }

            var updated = await _authorService.UpdateAuthorAsync(id, authorDto);

            if (updated == null)
            {
                return NotFound("Author not found.");
            }

            return Ok(updated);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAuthor(Guid id)
        {
            var isDeleted = await _authorService.DeleteAuthorAsync(id);

            if (!isDeleted)
            {
                return NotFound("Author not found.");
            }

            return NoContent();
        }
    }
}
