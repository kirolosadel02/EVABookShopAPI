using Microsoft.AspNetCore.Mvc;
using EVABookShopAPI.Service.Services.Categories;
using EVABookShopAPI.Service.DTOs.CategoryDTO;
using EVABookShopAPI.Service.DTOs;
using EVABookShopAPI.Service.Pagination;

namespace EVABookShopAPI.Controllers
{
    [ApiController]
    [Route("api/v1/categories")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<List<CategoryDto>>> GetAll() =>
            Ok(await _categoryService.GetAllCategoriesAsync());

        // New paginated endpoint
        [HttpGet("paginated")]
        public async Task<ActionResult<PaginatedResult<CategoryDto>>> GetPaginated([FromQuery] PaginationDto pagination)
        {
            if (pagination.Page < 1) pagination.Page = 1;
            if (pagination.PageSize < 1) pagination.PageSize = 5;
            if (pagination.PageSize > 100) pagination.PageSize = 100;

            return Ok(await _categoryService.GetPaginatedCategoriesAsync(pagination));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> GetById(int id) =>
            Ok(await _categoryService.GetCategoryByIdAsync(id));

        [HttpPost]
        public async Task<ActionResult<bool>> Create([FromBody] CategoryCreateDto model) =>
            Ok(await _categoryService.CreateCategoryAsync(model));

        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> Update(int id, [FromBody] CategoryUpdateDto model) =>
            Ok(await _categoryService.UpdateCategoryAsync(id, model));

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(int id) =>
            Ok(await _categoryService.DeleteCategoryAsync(id));

        [HttpGet("exists/name")]
        public async Task<ActionResult<bool>> CheckName([FromQuery] string categoryName, [FromQuery] int? excludeId) =>
            Ok(await _categoryService.CheckCategoryNameExistsAsync(categoryName, excludeId));

        [HttpGet("exists/order")]
        public async Task<ActionResult<bool>> CheckOrder([FromQuery] int categoryOrder, [FromQuery] int? excludeId) =>
            Ok(await _categoryService.CheckCategoryOrderExistsAsync(categoryOrder, excludeId));
    }
}