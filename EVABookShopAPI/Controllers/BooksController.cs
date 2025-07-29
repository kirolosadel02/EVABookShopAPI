using Microsoft.AspNetCore.Mvc;
using EVABookShopAPI.Service.DTOs.BookDTO;
using EVABookShopAPI.Service.Services.Books;

namespace EVABookShop.Controllers
{
    [ApiController]
    [Route("api/v1/books")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBooks() =>
            Ok(await _bookService.GetAllBooks());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            var book = await _bookService.GetBookById(id);
            return book == null ? NotFound() : Ok(book);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBook([FromBody] BookCreateDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _bookService.CreateBook(model);
            return result
                ? Ok(new { Message = "Book created successfully." })
                : NotFound("Category not found.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] BookUpdateDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _bookService.UpdateBook(id, model);
            return result
                ? Ok(new { Message = "Book updated successfully." })
                : NotFound("Book or Category not found.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var result = await _bookService.DeleteBook(id);
            return result
                ? Ok(new { Message = "Book deleted successfully." })
                : NotFound();
        }
    }
}
