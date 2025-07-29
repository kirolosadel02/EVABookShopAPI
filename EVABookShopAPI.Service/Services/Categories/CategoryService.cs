
using AutoMapper;
using EVABookShopAPI.DB.Models;
using EVABookShopAPI.UnitOfWork;
using EVABookShopAPI.Service.DTOs.CategoryDTO;
using EVABookShopAPI.Service.DTOs;
using EVABookShopAPI.Service.Pagination;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace EVABookShopAPI.Service.Services.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<CategoryDto>> GetAllCategoriesAsync()
        {
            var categories = await _unitOfWork.Repository<Category>().GetAll();
            return _mapper.Map<List<CategoryDto>>(categories);
        }

        public async Task<PaginatedResult<CategoryDto>> GetPaginatedCategoriesAsync(PaginationDto pagination)
        {
            var query = _unitOfWork.Repository<Category>()
                .GetAllQueryable()
                .OrderBy(c => c.CatOrder)
                .ThenByDescending(c => c.CatName);

            var totalCount = await query.CountAsync();

            var categories = await query
                .Skip(pagination.Skip)
                .Take(pagination.PageSize)
                .ToListAsync();

            var categoryDtos = _mapper.Map<List<CategoryDto>>(categories);

            return new PaginatedResult<CategoryDto>
            {
                Data = categoryDtos,
                TotalCount = totalCount,
                Page = pagination.Page,
                PageSize = pagination.PageSize
            };
        }

        public async Task<CategoryDto> GetCategoryByIdAsync(int id)
        {
            var category = await Task.Run(() => _unitOfWork.Repository<Category>().GetById(id));
            if (category == null) return null;

            return _mapper.Map<CategoryDto>(category);
        }

        public async Task<bool> CreateCategoryAsync(CategoryCreateDto model)
        {
            var repo = _unitOfWork.Repository<Category>();
            var existingCategory = (await repo.GetData(c => c.CatName == model.CatName)).FirstOrDefault();

            if (existingCategory != null)
            {
                if (existingCategory.MarkedAsDeleted)
                {
                    existingCategory.CatOrder = model.CatOrder;
                    existingCategory.MarkedAsDeleted = false;
                    await repo.Update(existingCategory);
                }
                else
                {
                    return false; // Name already exists
                }
            }
            else
            {
                var category = _mapper.Map<Category>(model);
                repo.Add(category);
            }

            await _unitOfWork.SaveChanges();
            return true;
        }

        public async Task<bool> UpdateCategoryAsync(int id, CategoryUpdateDto model)
        {
            var repo = _unitOfWork.Repository<Category>();
            var category = await Task.Run(() => repo.GetById(id));
            if (category == null)
                return false;

            if (category.CatName != model.CatName)
            {
                var nameExists = (await repo.GetData(c => c.CatName == model.CatName && c.Id != id && !c.MarkedAsDeleted)).Any();
                if (nameExists)
                {
                    return false; // Name already exists
                }
            }

            _mapper.Map(model, category);
            await repo.Update(category);
            await _unitOfWork.SaveChanges();
            return true;
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var repo = _unitOfWork.Repository<Category>();
            var category = await Task.Run(() => repo.GetById(id));
            if (category == null) return false;

            category.MarkedAsDeleted = true;
            await repo.Update(category);
            await _unitOfWork.SaveChanges();
            return true;
        }

        public async Task<bool> CheckCategoryNameExistsAsync(string categoryName, int? excludeId = null)
        {
            if (string.IsNullOrWhiteSpace(categoryName))
                return false;

            var repo = _unitOfWork.Repository<Category>();
            var query = await repo.GetData(c => c.CatName.ToLower().Trim() == categoryName.ToLower().Trim() && !c.MarkedAsDeleted);
            if (excludeId.HasValue)
                query = query.Where(c => c.Id != excludeId.Value);

            return query.Any();
        }

        public async Task<bool> CheckCategoryOrderExistsAsync(int categoryOrder, int? excludeId = null)
        {
            var repo = _unitOfWork.Repository<Category>();
            var query = await repo.GetData(c => c.CatOrder == categoryOrder && !c.MarkedAsDeleted);
            if (excludeId.HasValue)
                query = query.Where(c => c.Id != excludeId.Value);

            return query.Any();
        }
    }
}