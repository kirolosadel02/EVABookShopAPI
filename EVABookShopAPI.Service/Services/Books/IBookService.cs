using EVABookShopAPI.Service.DTOs.BookDTO.EVABookShop.DTOs;
using EVABookShopAPI.Service.DTOs.BookDTO;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace EVABookShopAPI.Service.Services.Books
{
    public interface IBookService
    {
        Task<List<BookDto>> GetAllBooks();
        Task<BookDto> GetBookById(int id);
        Task<bool> CreateBook(BookCreateDto model);
        Task<bool> UpdateBook(int id, BookUpdateDto model);
        Task<bool?> PatchBook(int id, JsonPatchDocument<BookUpdateDto> patchDoc, ModelStateDictionary modelState);
        Task<bool> DeleteBook(int id);
    }
}
