using AutoMapper;
using EVABookShopAPI.DB.Models;
using EVABookShopAPI.Service.DTOs.BookDTO;
using EVABookShopAPI.Service.DTOs.BookDTO.EVABookShop.DTOs;

namespace EVABookShopAPI.Service.Mappings
{
    public class BookMappingProfile : Profile
    {
        public BookMappingProfile()
        {
            // Book → BookDto
            CreateMap<Book, BookDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category != null ? src.Category.CatName : string.Empty));

            // BookCreateDto → Book
            CreateMap<BookCreateDto, Book>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CategoryId, opt => opt.Ignore()); // resolved from CategoryName

            // BookUpdateDto → Book
            CreateMap<BookUpdateDto, Book>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CategoryId, opt => opt.Ignore());

            // Optional: Book → BookUpdateDto (for edit pre-fill)
            CreateMap<Book, BookUpdateDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category != null ? src.Category.CatName : string.Empty));
        }
    }
}
