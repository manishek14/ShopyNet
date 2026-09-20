using Common.Application.Validation;
using FluentValidation;

namespace Shop.Application.Products.AddImage
{
    public class AddProductImageCommandValidator : AbstractValidator<AddProductImageCommand>
    {
        public AddProductImageCommandValidator()
        {
            RuleFor(x => x.ProductId)
                .NotEmpty()
                    .WithMessage(ValidationMessages.required("ProductId"));

            RuleFor(x => x.ImageFile)
                .NotNull()
                    .WithMessage(ValidationMessages.required("ImageFile"))
                .Must(file => file != null && file.Length > 0)
                    .WithMessage("Image file cannot be empty")
                .Must(file => file == null || file.Length <= 5 * 1024 * 1024)
                    .WithMessage("Image size cannot exceed 5MB");

            RuleFor(x => x.Sequence)
                .NotEmpty()
                    .WithMessage(ValidationMessages.required("Sequence"))
                .MaximumLength(50)
                    .WithMessage(ValidationMessages.maxLength("Sequence", 50));
        }
    }
}