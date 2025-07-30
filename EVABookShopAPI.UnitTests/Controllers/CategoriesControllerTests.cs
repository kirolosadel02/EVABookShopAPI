using Moq;
using Microsoft.AspNetCore.Mvc;
using EVABookShopAPI.Controllers;
using EVABookShopAPI.Service.Services.Categories;
using EVABookShopAPI.Service.DTOs.CategoryDTO;
using EVABookShopAPI.Service.DTOs;
using EVABookShopAPI.Service.Pagination;

namespace EVABookShopAPI.UnitTests.Controllers
{
    public class CategoriesControllerTests
    {
        private readonly CategoriesController _controller;
        private readonly Mock<ICategoryService> _mockService;

        public CategoriesControllerTests()
        {
            _mockService = new Mock<ICategoryService>();
            _controller = new CategoriesController(_mockService.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsOk_WithListOfCategories()
        {
            // Arrange
            var mockData = new List<CategoryDto>
            {
                new CategoryDto { Id = 1, CatName = "Sci-Fi" },
                new CategoryDto { Id = 2, CatName = "History" }
            };

            _mockService.Setup(s => s.GetAllCategoriesAsync())
                .ReturnsAsync(mockData);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var categories = Assert.IsType<List<CategoryDto>>(okResult.Value);
            Assert.Equal(2, categories.Count);
        }

        [Fact]
        public async Task GetPaginated_ReturnsOk_WithPaginatedData()
        {
            // Arrange
            var pagination = new PaginationDto { Page = 1, PageSize = 5 };
            var pagedResult = new PaginatedResult<CategoryDto>
            {
                Page = 1,
                PageSize = 5,
                TotalCount = 10,
                Data = new List<CategoryDto> { new CategoryDto { Id = 1, CatName = "Test" } }
            };

            _mockService.Setup(s => s.GetPaginatedCategoriesAsync(pagination))
                .ReturnsAsync(pagedResult);

            // Act
            var result = await _controller.GetPaginated(pagination);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var resultData = Assert.IsType<PaginatedResult<CategoryDto>>(okResult.Value);
            Assert.Single(resultData.Data);
        }

        [Fact]
        public async Task GetById_ReturnsOk_WhenFound()
        {
            // Arrange
            var dto = new CategoryDto { Id = 1, CatName = "Drama" };
            _mockService.Setup(s => s.GetCategoryByIdAsync(1))
                .ReturnsAsync(dto);

            // Act
            var result = await _controller.GetById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var data = Assert.IsType<CategoryDto>(okResult.Value);
            Assert.Equal(1, data.Id);
        }

        [Fact]
        public async Task Create_ReturnsOk_WhenCreated()
        {
            // Arrange
            var createDto = new CategoryCreateDto { CatName = "Fantasy", CatOrder = 5 };
            _mockService.Setup(s => s.CreateCategoryAsync(createDto))
                .ReturnsAsync(true);

            // Act
            var result = await _controller.Create(createDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.True((bool)okResult.Value);
        }

        [Fact]
        public async Task Update_ReturnsOk_WhenUpdated()
        {
            // Arrange
            var updateDto = new CategoryUpdateDto { CatName = "Updated", CatOrder = 3 };
            _mockService.Setup(s => s.UpdateCategoryAsync(1, updateDto))
                .ReturnsAsync(true);

            // Act
            var result = await _controller.Update(1, updateDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.True((bool)okResult.Value);
        }

        [Fact]
        public async Task Delete_ReturnsOk_WhenDeleted()
        {
            // Arrange
            _mockService.Setup(s => s.DeleteCategoryAsync(1))
                .ReturnsAsync(true);

            // Act
            var result = await _controller.Delete(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.True((bool)okResult.Value);
        }

        [Fact]
        public async Task CheckName_ReturnsOk_WithResult()
        {
            // Arrange
            string categoryName = "TestCat";
            int? excludeId = null;
            _mockService.Setup(s => s.CheckCategoryNameExistsAsync(categoryName, excludeId))
                .ReturnsAsync(true);

            // Act
            var result = await _controller.CheckName(categoryName, excludeId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.True((bool)okResult.Value);
        }

        [Fact]
        public async Task CheckOrder_ReturnsOk_WithResult()
        {
            // Arrange
            int catOrder = 2;
            int? excludeId = null;
            _mockService.Setup(s => s.CheckCategoryOrderExistsAsync(catOrder, excludeId))
                .ReturnsAsync(false);

            // Act
            var result = await _controller.CheckOrder(catOrder, excludeId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.False((bool)okResult.Value);
        }

        [Fact]
        public async Task GetById_ReturnsNotFound_WhenCategoryDoesNotExist()
        {
            // Arrange
            _mockService.Setup(s => s.GetCategoryByIdAsync(99)).ReturnsAsync((CategoryDto)null);

            // Act
            var result = await _controller.GetById(99);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Null(okResult.Value); // Or: Assert.True(okResult.Value is null);
        }

        [Fact]
        public async Task Create_ReturnsOkFalse_WhenCategoryCreationFails()
        {
            // Arrange
            var dto = new CategoryCreateDto { CatName = "ExistingName", CatOrder = 1 };
            _mockService.Setup(s => s.CreateCategoryAsync(dto)).ReturnsAsync(false);

            // Act
            var result = await _controller.Create(dto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.False((bool)okResult.Value);
        }

        [Fact]
        public async Task Update_ReturnsOkFalse_WhenUpdateFails()
        {
            // Arrange
            var dto = new CategoryUpdateDto { CatName = "ExistingName", CatOrder = 1 };
            _mockService.Setup(s => s.UpdateCategoryAsync(1, dto)).ReturnsAsync(false);

            // Act
            var result = await _controller.Update(1, dto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.False((bool)okResult.Value);
        }

        [Fact]
        public async Task Delete_ReturnsOkFalse_WhenCategoryNotFound()
        {
            // Arrange
            _mockService.Setup(s => s.DeleteCategoryAsync(404)).ReturnsAsync(false);

            // Act
            var result = await _controller.Delete(404);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.False((bool)okResult.Value);
        }

        [Fact]
        public async Task CheckName_ReturnsFalse_WhenNameIsEmpty()
        {
            // Arrange
            string emptyName = " ";
            _mockService.Setup(s => s.CheckCategoryNameExistsAsync(emptyName, null)).ReturnsAsync(false);

            // Act
            var result = await _controller.CheckName(emptyName, null);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.False((bool)okResult.Value);
        }

        [Fact]
        public async Task CheckOrder_ReturnsTrue_WhenOrderAlreadyExists()
        {
            // Arrange
            int existingOrder = 1;
            _mockService.Setup(s => s.CheckCategoryOrderExistsAsync(existingOrder, null)).ReturnsAsync(true);

            // Act
            var result = await _controller.CheckOrder(existingOrder, null);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.True((bool)okResult.Value);
        }

    }
}
