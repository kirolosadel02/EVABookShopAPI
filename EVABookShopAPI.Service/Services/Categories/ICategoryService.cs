using EVABookShopAPI.Service.DTOs.CategoryDTO;

namespace EVABookShopAPI.Service.Services.Categories
{
    public interface ICategoryService
    {
        Task<List<CategoryDto>> GetAllCategoriesAsync();
        Task<CategoryDto> GetCategoryByIdAsync(int id);
        Task<bool> CreateCategoryAsync(CategoryCreateDto model);
        Task<bool> UpdateCategoryAsync(int id, CategoryUpdateDto model);
        Task<bool> DeleteCategoryAsync(int id);
        Task<bool> CheckCategoryNameExistsAsync(string categoryName, int? excludeId = null);
        Task<bool> CheckCategoryOrderExistsAsync(int categoryOrder, int? excludeId = null);
    }
}