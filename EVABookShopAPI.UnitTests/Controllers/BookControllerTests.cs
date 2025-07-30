using Moq;
using Microsoft.AspNetCore.Mvc;
using EVABookShopAPI.Service.DTOs.BookDTO;
using EVABookShopAPI.Service.Services.Books;
using EVABookShop.Controllers;
using EVABookShopAPI.Service.DTOs.BookDTO.EVABookShop.DTOs;

namespace EVABookShopAPI.UnitTests.Controllers
{
    public class BookControllerTests
    {
        private readonly BookController _controller;
        private readonly Mock<IBookService> _bookServiceMock;

        public BookControllerTests()
        {
            _bookServiceMock = new Mock<IBookService>();
            _controller = new BookController(_bookServiceMock.Object);
        }

        [Fact]
        public async Task GetAllBooks_ReturnsOk_WithListOfBooks()
        {
            // Arrange
            var books = new List<BookDto> { new BookDto { Id = 1, Title = "Book A" } };
            _bookServiceMock.Setup(s => s.GetAllBooks()).ReturnsAsync(books);

            // Act
            var result = await _controller.GetAllBooks();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnBooks = Assert.IsType<List<BookDto>>(okResult.Value);
            Assert.Single(returnBooks);
        }

        [Fact]
        public async Task GetBookById_ReturnsNotFound_WhenBookDoesNotExist()
        {
            // Arrange
            _bookServiceMock.Setup(s => s.GetBookDtoResultById(1)).ReturnsAsync(new NotFoundResult());

            // Act
            var result = await _controller.GetBookById(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task CreateBook_ReturnsOk_WhenBookIsCreated()
        {
            // Arrange
            var dto = new BookCreateDto { Title = "Book A", Author = "Author", CategoryName = "Category", Price = 10 };
            _bookServiceMock.Setup(s => s.CreateBookResult(dto)).ReturnsAsync(new OkObjectResult(new { Message = "Book created successfully." }));

            // Act
            var result = await _controller.CreateBook(dto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Book created successfully.", ((dynamic)okResult.Value).Message);
        }

        [Fact]
        public async Task UpdateBook_ReturnsNotFound_WhenBookNotFound()
        {
            // Arrange
            var dto = new BookUpdateDto { Title = "Updated Title", CategoryName = "UpdatedCat" };
            _bookServiceMock.Setup(s => s.UpdateBookResult(1, dto)).ReturnsAsync(new NotFoundObjectResult("Book or Category not found."));

            // Act
            var result = await _controller.UpdateBook(1, dto);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Book or Category not found.", notFoundResult.Value);
        }

        [Fact]
        public async Task DeleteBook_ReturnsOk_WhenBookDeleted()
        {
            // Arrange
            _bookServiceMock.Setup(s => s.DeleteBookResult(1)).ReturnsAsync(new OkObjectResult(new { Message = "Book deleted successfully." }));

            // Act
            var result = await _controller.DeleteBook(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Book deleted successfully.", ((dynamic)okResult.Value).Message);
        }

        [Fact]
        public async Task DeleteBook_ReturnsNotFound_WhenBookNotFound()
        {
            // Arrange
            _bookServiceMock.Setup(s => s.DeleteBookResult(999)).ReturnsAsync(new NotFoundResult());

            // Act
            var result = await _controller.DeleteBook(999);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
