using FluentValidation;
using EVABookShopAPI.UnitOfWork;

namespace EVABookShopAPI.Service.DTOs.CategoryDTO.Validators
{
    public class CategoryCreateDtoValidator : AbstractValidator<CategoryCreateDto>
    {
        public CategoryCreateDtoValidator(IUnitOfWork unitOfWork)
        {
            RuleFor(c => c.CatName)
                .NotEmpty().WithMessage("Category name is required.")
                .MinimumLength(2).WithMessage("Category name can't be less than 2 characters.")
                .MaximumLength(50).WithMessage("Category name cannot exceed 50 characters.");

            RuleFor(c => c.CatOrder)
                .NotNull().WithMessage("Category order is required.")
                .GreaterThanOrEqualTo(0).WithMessage("Category order must be non-negative.");
        }
    }
}
