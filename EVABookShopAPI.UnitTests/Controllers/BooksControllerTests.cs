using Moq;
using Microsoft.AspNetCore.Mvc;
using EVABookShopAPI.Service.DTOs.BookDTO;
using EVABookShopAPI.Service.Services.Books;
using EVABookShop.Controllers;
using EVABookShopAPI.Service.DTOs.BookDTO.EVABookShop.DTOs;
using Microsoft.AspNetCore.JsonPatch;

namespace EVABookShopAPI.UnitTests.Controllers
{
    public class BooksControllerTests
    {
        private readonly BookController _controller;
        private readonly Mock<IBookService> _bookServiceMock;

        public BooksControllerTests()
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

        [Fact]
        public async Task CreateBook_ReturnsNotFound_WhenCategoryDoesNotExist()
        {
            // Arrange
            var dto = new BookCreateDto
            {
                Title = "New Book",
                Author = "Author",
                CategoryName = "NonExistingCategory",
                Price = 20
            };

            _bookServiceMock.Setup(s => s.CreateBookResult(dto))
                .ReturnsAsync(new NotFoundObjectResult("Category not found."));

            // Act
            var result = await _controller.CreateBook(dto);

            // Assert
            var notFound = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Category not found.", notFound.Value);
        }

        [Fact]
        public async Task CreateBook_ReturnsBadRequest_WhenModelIsInvalid()
        {
            // Arrange
            var dto = new BookCreateDto
            {
                Title = "", // Invalid
                Author = "Valid",
                CategoryName = "Category",
                Price = 50
            };

            var errors = new Dictionary<string, string[]> { { "Title", new[] { "Title is required." } } };
            _bookServiceMock.Setup(s => s.CreateBookResult(dto))
                .ReturnsAsync(new BadRequestObjectResult(errors));

            // Act
            var result = await _controller.CreateBook(dto);

            // Assert
            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            var errorDict = Assert.IsType<Dictionary<string, string[]>>(badRequest.Value);
            Assert.True(errorDict.ContainsKey("Title"));
        }


        [Fact]
        public async Task UpdateBook_ReturnsBadRequest_WhenModelIsInvalid()
        {
            // Arrange
            var dto = new BookUpdateDto
            {
                Title = "Valid",
                Author = "", // Invalid
                CategoryName = "Category",
                Price = 100
            };

            var errors = new Dictionary<string, string[]> { { "Author", new[] { "Author is required." } } };
            _bookServiceMock.Setup(s => s.UpdateBookResult(1, dto))
                .ReturnsAsync(new BadRequestObjectResult(errors));

            // Act
            var result = await _controller.UpdateBook(1, dto);

            // Assert
            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            var errorDict = Assert.IsType<Dictionary<string, string[]>>(badRequest.Value);
            Assert.True(errorDict.ContainsKey("Author"));
        }


        [Fact]
        public async Task UpdateBook_ReturnsNotFound_WhenCategoryDoesNotExist()
        {
            // Arrange
            var dto = new BookUpdateDto
            {
                Title = "Updated",
                Author = "Author",
                CategoryName = "InvalidCategory",
                Price = 15
            };

            _bookServiceMock.Setup(s => s.UpdateBookResult(99, dto))
                .ReturnsAsync(new NotFoundObjectResult("Book or Category not found."));

            // Act
            var result = await _controller.UpdateBook(99, dto);

            // Assert
            var notFound = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Book or Category not found.", notFound.Value);
        }

        [Fact]
        public async Task GetBookById_ReturnsBadRequest_WhenIdIsNegative()
        {
            // Arrange
            _bookServiceMock
                .Setup(s => s.GetBookDtoResultById(-1))
                .ReturnsAsync(new BadRequestResult());

            // Act
            var result = await _controller.GetBookById(-1);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }


        [Fact]
        public async Task CreateBook_ReturnsBadRequest_WhenPriceIsOutOfRange()
        {
            // Arrange
            var dto = new BookCreateDto
            {
                Title = "Valid",
                Author = "Author",
                CategoryName = "Category",
                Price = 2000 // Out of range
            };

            var errors = new Dictionary<string, string[]> { { "Price", new[] { "Price must be between 1 and 1000." } } };
            _bookServiceMock.Setup(s => s.CreateBookResult(dto))
                .ReturnsAsync(new BadRequestObjectResult(errors));

            // Act
            var result = await _controller.CreateBook(dto);

            // Assert
            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            var errorDict = Assert.IsType<Dictionary<string, string[]>>(badRequest.Value);
            Assert.True(errorDict.ContainsKey("Price"));
        }

        [Fact]
        public async Task DeleteBook_ReturnsBadRequest_WhenIdIsNegative()
        {
            // Arrange
            _bookServiceMock.Setup(s => s.DeleteBookResult(-1))
                .ReturnsAsync(new BadRequestResult());

            // Act
            var result = await _controller.DeleteBook(-1);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task UpdateBook_ReturnsBadRequest_WhenIdIsNegative()
        {
            // Arrange
            var dto = new BookUpdateDto
            {
                Title = "Test",
                Author = "Author",
                CategoryName = "Category",
                Price = 50
            };

            _bookServiceMock.Setup(s => s.UpdateBookResult(-1, dto))
                .ReturnsAsync(new BadRequestResult());

            // Act
            var result = await _controller.UpdateBook(-1, dto);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task PatchBook_ReturnsBadRequest_WhenIdIsNegative()
        {
            // Arrange
            var patchDoc = new JsonPatchDocument<BookUpdateDto>();
            patchDoc.Replace(b => b.Title, "Patched Title");

            _bookServiceMock.Setup(s => s.PatchBookResult(-1, patchDoc))
                .ReturnsAsync(new BadRequestResult());

            // Act
            var result = await _controller.PatchBook(-1, patchDoc);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

    }
}
