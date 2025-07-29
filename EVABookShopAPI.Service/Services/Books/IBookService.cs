using EVABookShopAPI.Service.DTOs.BookDTO.EVABookShop.DTOs;
using EVABookShopAPI.Service.DTOs.BookDTO;

namespace EVABookShopAPI.Service.Services.Books
{
    public interface IBookService
    {
        Task<List<BookDto>> GetAllBooks();
        Task<BookDto> GetBookById(int id);
        Task<bool> CreateBook(BookCreateDto model);
        Task<bool> UpdateBook(int id, BookUpdateDto model);
        Task<bool> DeleteBook(int id);
    }
}
