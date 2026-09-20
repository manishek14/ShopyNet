using Common.Application.Validation;
using FluentValidation;

namespace Shop.Application.Products.RemoveImage
{
    public class RemoveProductImageCommandValidator : AbstractValidator<RemoveProductImageCommand>
    {
        public RemoveProductImageCommandValidator()
        {
            RuleFor(x => x.ProductId)
                .NotEmpty()
                    .WithMessage(ValidationMessages.required("ProductId"));

            RuleFor(x => x.ImageId)
                .NotEmpty()
                    .WithMessage(ValidationMessages.required("ImageId"));
        }
    }
}