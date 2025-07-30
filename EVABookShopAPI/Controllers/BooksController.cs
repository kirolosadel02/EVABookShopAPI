using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using EVABookShopAPI.Service.DTOs.BookDTO;
using EVABookShopAPI.Service.Services.Books;

namespace EVABookShop.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/books")]
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
        public async Task<IActionResult> GetBookById(int id) =>
            await _bookService.GetBookDtoResultById(id);

        [HttpPost]
        public async Task<IActionResult> CreateBook([FromBody] BookCreateDto model) =>
            await _bookService.CreateBookResult(model, ModelState);

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] BookUpdateDto model) =>
            await _bookService.UpdateBookResult(id, model, ModelState);

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchBook(int id, [FromBody] JsonPatchDocument<BookUpdateDto> patchDoc) =>
            await _bookService.PatchBookResult(id, patchDoc, ModelState);

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id) =>
            await _bookService.DeleteBookResult(id);
    }
}
