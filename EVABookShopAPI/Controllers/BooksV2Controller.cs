using Microsoft.AspNetCore.Mvc;
using EVABookShopAPI.Service.Services.Books;

namespace EVABookShop.Controllers.V2
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/books")]
    public class BookV2Controller : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookV2Controller(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        [ResponseCache(CacheProfileName = "Server60")]
        public async Task<IActionResult> GetAllBooks()
        {
            var books = await _bookService.GetAllBooks();
            // V2 might include additional metadata
            return Ok(new
            {
                Version = "2.0",
                Data = books,
                Count = books.Count,
                Timestamp = DateTime.UtcNow
            });
        }

        [HttpGet("{id}")]
        [ResponseCache(CacheProfileName = "Default30")]
        public async Task<IActionResult> GetBookById(int id)
        {
            var book = await _bookService.GetBookById(id);
            if (book == null)
                return NotFound();

            return Ok(new
            {
                Version = "2.0",
                Data = book,
                RetrievedAt = DateTime.UtcNow
            });
        }

        // Other methods would follow similar pattern...
    }
}