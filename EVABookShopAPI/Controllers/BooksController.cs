using Microsoft.AspNetCore.JsonPatch;
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
        [ResponseCache(CacheProfileName = "Server60")]
        public async Task<IActionResult> GetAllBooks() =>
            Ok(await _bookService.GetAllBooks());

        [HttpGet("{id}")]
        [ResponseCache(CacheProfileName = "Default30")]
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

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchBook(int id, [FromBody] JsonPatchDocument<BookUpdateDto> patchDoc)
        {
            if (patchDoc == null)
                return BadRequest("Patch document is null.");

            var result = await _bookService.PatchBook(id, patchDoc, ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return result switch
            {
                true => Ok(new { Message = "Book updated successfully." }),
                false => NotFound("Book or Category not found."),
                null => BadRequest("Invalid patch operation.")
            };
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
