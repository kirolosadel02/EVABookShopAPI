using FluentValidation;

namespace EVABookShopAPI.Service.DTOs.BookDTO.Validators
{
    public class BookUpdateDtoValidator : AbstractValidator<BookUpdateDto>
    {
        public BookUpdateDtoValidator()
        {
            RuleFor(b => b.Title)
                .NotEmpty().WithMessage("Title is required.")
                .Length(2, 50).WithMessage("Title must be between 2 and 50 characters.");

            RuleFor(b => b.Description)
                .MaximumLength(250).WithMessage("Description can't exceed 250 characters.");

            RuleFor(b => b.Author)
                .NotEmpty().WithMessage("Author is required.")
                .Length(2, 50).WithMessage("Author must be between 2 and 50 characters.");

            RuleFor(b => b.Price)
                .InclusiveBetween(1, 1000).WithMessage("Price must be between 1 and 1000.");

            RuleFor(b => b.CategoryName)
                .NotEmpty().WithMessage("Category name is required.");
        }
    }
}
