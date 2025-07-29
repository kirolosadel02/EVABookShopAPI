using AutoMapper;
using EVABookShopAPI.DB.Models;
using EVABookShopAPI.Service.DTOs.CategoryDTO;

namespace EVABookShopAPI.Service.Mappings
{
    public class CategoryMappingProfile : Profile
    {
        public CategoryMappingProfile()
        {
            // Category to CategoryDto
            CreateMap<Category, CategoryDto>()
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => !src.MarkedAsDeleted))
                .ForMember(dest => dest.CatName, opt => opt.MapFrom(src => src.CatName ?? string.Empty));

            // CategoryCreateDto to Category
            CreateMap<CategoryCreateDto, Category>()
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.MarkedAsDeleted, opt => opt.MapFrom(src => false))
                .ForMember(dest => dest.Books, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            // CategoryUpdateDto to Category (for updates)
            CreateMap<CategoryUpdateDto, Category>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.MarkedAsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.Books, opt => opt.Ignore());

            // Reverse mapping for updates (Category to CategoryUpdateDto)
            CreateMap<Category, CategoryUpdateDto>();
        }
    }
} 