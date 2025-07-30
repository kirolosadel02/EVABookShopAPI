using Microsoft.AspNetCore.Mvc;
using EVABookShopAPI.Service.DTOs.CategoryDTO;
using EVABookShopAPI.Service.DTOs;
using EVABookShopAPI.Service.Services.Categories;

namespace EVABookShopAPI.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/categories")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await _categoryService.GetAllCategoriesAsync());

        [HttpGet("paginated")]
        public async Task<IActionResult> GetPaginated([FromQuery] PaginationDto pagination) =>
            Ok(await _categoryService.GetPaginatedCategoriesAsync(pagination));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) =>
            Ok(await _categoryService.GetCategoryByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CategoryCreateDto model) =>
            Ok(await _categoryService.CreateCategoryAsync(model));

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CategoryUpdateDto model) =>
            Ok(await _categoryService.UpdateCategoryAsync(id, model));

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) =>
            Ok(await _categoryService.DeleteCategoryAsync(id));

        [HttpGet("exists/name")]
        public async Task<IActionResult> CheckName([FromQuery] string categoryName, [FromQuery] int? excludeId) =>
            Ok(await _categoryService.CheckCategoryNameExistsAsync(categoryName, excludeId));

        [HttpGet("exists/order")]
        public async Task<IActionResult> CheckOrder([FromQuery] int categoryOrder, [FromQuery] int? excludeId) =>
            Ok(await _categoryService.CheckCategoryOrderExistsAsync(categoryOrder, excludeId));
    }
}
