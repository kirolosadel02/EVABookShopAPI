using FluentValidation;

namespace EVABookShopAPI.Service.DTOs.BookDTO.Validators
{
    public class BookCreateDtoValidator : AbstractValidator<BookCreateDto>
    {
        public BookCreateDtoValidator()
        {
            RuleFor(b => b.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MinimumLength(2).WithMessage("Title can't be less than 2 characters.")
                .MaximumLength(50).WithMessage("Title can't exceed 50 characters.");

            RuleFor(b => b.Description)
                .MaximumLength(250).WithMessage("Description can't exceed 250 characters.");

            RuleFor(b => b.Author)
                .NotEmpty().WithMessage("Author is required.")
                .MinimumLength(2).WithMessage("Author can't be less than 2 characters")
                .MaximumLength(50).WithMessage("Author can't exceed 50 characters.");

            RuleFor(b => b.Price)
                .InclusiveBetween(1, 1000).WithMessage("Price must be between 1 and 1000.");

            RuleFor(b => b.CategoryName)
                .NotEmpty().WithMessage("Category name is required.");
        }
    }
}
