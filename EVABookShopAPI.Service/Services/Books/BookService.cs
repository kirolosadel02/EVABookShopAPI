using EVABookShopAPI.DB.Models;
using EVABookShopAPI.Service.DTOs.BookDTO.EVABookShop.DTOs;
using EVABookShopAPI.Service.DTOs.BookDTO;
using EVABookShopAPI.UnitOfWork;

namespace EVABookShopAPI.Service.Services.Books
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BookService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<BookDto>> GetAllBooks()
        {
            var books = await _unitOfWork.Repository<Book>().GetAll(new List<string> { "Category" });

            return books.Select(b => new BookDto
            {
                Id = b.Id,
                Title = b.Title,
                Description = b.Description,
                Author = b.Author,
                Price = b.Price,
                CategoryId = b.CategoryId,
                CategoryName = b.Category?.CatName ?? string.Empty
            }).ToList();
        }

        public async Task<BookDto?> GetBookById(int id)
        {
            var book = _unitOfWork.Repository<Book>().GetById(id, new List<string> { "Category" });
            if (book == null) return null;

            return new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                Description = book.Description,
                Author = book.Author,
                Price = book.Price,
                CategoryId = book.CategoryId,
                CategoryName = book.Category?.CatName ?? string.Empty
            };
        }

        public async Task<bool> CreateBook(BookCreateDto model)
        {
            var category = _unitOfWork.Repository<Category>().GetAll().Result
                .FirstOrDefault(c => c.CatName.ToLower() == model.CategoryName.ToLower());

            if (category == null)
                return false;

            var book = new Book
            {
                Title = model.Title,
                Description = model.Description,
                Author = model.Author,
                Price = model.Price,
                CategoryId = category.Id
            };

            _unitOfWork.Repository<Book>().Add(book);
            await _unitOfWork.SaveChanges();
            return true;
        }

        public async Task<bool> UpdateBook(int id, BookUpdateDto model)
        {
            var book = _unitOfWork.Repository<Book>().GetById(id);
            if (book == null)
                return false;

            var category = _unitOfWork.Repository<Category>().GetAll().Result
                .FirstOrDefault(c => c.CatName.ToLower() == model.CategoryName.ToLower());

            if (category == null)
                return false;

            book.Title = model.Title;
            book.Description = model.Description;
            book.Author = model.Author;
            book.Price = model.Price;
            book.CategoryId = category.Id;

            await _unitOfWork.Repository<Book>().Update(book);
            await _unitOfWork.SaveChanges();
            return true;
        }

        public async Task<bool> DeleteBook(int id)
        {
            var book = _unitOfWork.Repository<Book>().GetById(id);
            if (book == null) return false;

            _unitOfWork.Repository<Book>().Delete(book.Id);
            await _unitOfWork.SaveChanges();
            return true;
        }
    }
}
