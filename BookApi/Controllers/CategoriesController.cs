using BookApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private const int DefaultPageNumber = 1;
        private const int DefaultPageSize = 10;
        public CategoriesController(ICategoryService сategoryService)
        {
            _categoryService = сategoryService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategorys(int pageNumber = DefaultPageNumber, int pageSize = DefaultPageSize)
        {
            if (pageNumber < 1 || pageSize < 1)
            {
                return BadRequest("Page number and page size must be greater than 0.");
            }

            var categories = await _categoryService.GetCategoriesAsync();

            if (categories == null || !categories.Any())
            {
                return NotFound("No categories found.");
            }

            var paginated = categories
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return Ok(paginated);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> GetCategoryById(Guid id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);

            if (category == null)
            {
                return NotFound($"Category with id {id} not found.");
            }

            return Ok(category);
        }
        [HttpPost]
        public async Task<ActionResult<CategoryDto>> CreateCategory([FromBody] CategoryDto categoryDto)
        {
            if (categoryDto == null)
            {
                return BadRequest("Invalid data.");
            }

            var created = await _categoryService.CreateCategoryAsync(categoryDto);

            return CreatedAtAction(nameof(GetCategoryById), new { id = created.CategoryId }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CategoryDto>> UpdateCategory(Guid id, [FromBody] CategoryDto categoryDto)
        {
            if (categoryDto == null)
            {
                return BadRequest("Invalid data.");
            }

            var updated = await _categoryService.UpdateCategoryAsync(id, categoryDto);

            if (updated == null)
            {
                return NotFound("Category not found.");
            }

            return Ok(updated);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCategory(Guid id)
        {
            var isDeleted = await _categoryService.DeleteCategoryAsync(id);

            if (!isDeleted)
            {
                return NotFound("Category not found.");
            }

            return NoContent();
        }
    }
}
