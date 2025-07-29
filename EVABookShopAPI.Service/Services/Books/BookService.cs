using AutoMapper;
using EVABookShopAPI.DB.Models;
using EVABookShopAPI.Service.DTOs.BookDTO;
using EVABookShopAPI.Service.DTOs.BookDTO.EVABookShop.DTOs;
using EVABookShopAPI.UnitOfWork;

namespace EVABookShopAPI.Service.Services.Books
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BookService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<BookDto>> GetAllBooks()
        {
            var books = await _unitOfWork.Repository<Book>().GetAll(new List<string> { "Category" });
            return _mapper.Map<List<BookDto>>(books);
        }

        public async Task<BookDto?> GetBookById(int id)
        {
            var book = _unitOfWork.Repository<Book>().GetById(id, new List<string> { "Category" });
            return book == null ? null : _mapper.Map<BookDto>(book);
        }

        public async Task<bool> CreateBook(BookCreateDto model)
        {
            var category = _unitOfWork.Repository<Category>().GetAll().Result
                .FirstOrDefault(c => c.CatName.ToLower() == model.CategoryName.ToLower());

            if (category == null)
                return false;

            var book = _mapper.Map<Book>(model);
            book.CategoryId = category.Id;

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

            _mapper.Map(model, book);
            book.CategoryId = category.Id;

            await _unitOfWork.Repository<Book>().Update(book);
            await _unitOfWork.SaveChanges();
            return true;
        }

        public async Task<bool> DeleteBook(int id)
        {
            var book = _unitOfWork.Repository<Book>().GetById(id);
            if (book == null)
                return false;

            _unitOfWork.Repository<Book>().Delete(book.Id);
            await _unitOfWork.SaveChanges();
            return true;
        }
    }
}
