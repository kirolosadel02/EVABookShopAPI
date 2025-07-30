using EVABookShopAPI.Service.DTOs.BookDTO.EVABookShop.DTOs;
using EVABookShopAPI.Service.DTOs.BookDTO;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;

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
        Task<IActionResult> CreateBookResult(BookCreateDto model, ModelStateDictionary modelState);
        Task<IActionResult> UpdateBookResult(int id, BookUpdateDto model, ModelStateDictionary modelState);
        Task<IActionResult> PatchBookResult(int id, JsonPatchDocument<BookUpdateDto> patchDoc, ModelStateDictionary modelState);
        Task<IActionResult> DeleteBookResult(int id);
        Task<IActionResult> GetBookDtoResultById(int id);

    }
}
