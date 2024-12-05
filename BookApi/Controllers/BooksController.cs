using BookApi.Data;
using BookApi.Models;
using BookApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BookApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;
        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetBooks(int pageNumber = 1, int pageSize = 10)
        {
            if (pageNumber < 1 || pageSize < 1)
            {
                return BadRequest("Page number and page size must be greater than 0.");
            }

            var books = await _bookService.GetBooksAsync();

            if (books == null || !books.Any())
            {
                return NotFound("No books found.");
            }

            var paginatedBooks = books
                .Skip((pageNumber - 1) * pageSize)  
                .Take(pageSize) 
                .ToList();

            return Ok(paginatedBooks);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookDto>> GetBookById(Guid id)
        {
            var book = await _bookService.GetBookByIdAsync(id);

            if (book == null)
            {
                return NotFound($"Book with id {id} not found.");
            }

            return Ok(book);
        }

        [HttpPost]
        public async Task<ActionResult<BookDto>> CreateBook([FromBody] BookDto bookDto)
        {
            if (bookDto == null)
            {
                return BadRequest("Invalid data.");
            }

            var createdBook = await _bookService.CreateBookAsync(bookDto);

            return CreatedAtAction(nameof(GetBookById), new { id = createdBook.BookId }, createdBook);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<BookDto>> UpdateBook(Guid id, [FromBody] BookDto bookDto)
        {
            if (bookDto == null)
            {
                return BadRequest("Invalid data.");
            }

            var updatedBook = await _bookService.UpdateBookAsync(id, bookDto);

            if (updatedBook == null)
            {
                return NotFound("Book not found.");
            }

            return Ok(updatedBook);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBook(Guid id)
        {
            var isDeleted = await _bookService.DeleteBookAsync(id);

            if (!isDeleted)
            {
                return NotFound("Book not found.");
            }

            return NoContent(); 
        }


    }
}
