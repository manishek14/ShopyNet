using Common.Application.Validation;
using FluentValidation;

namespace Shop.Application.Sellers.Create
{
    public class CreateSellerCommandValidator : AbstractValidator<CreateSellerCommand>
    {
        public CreateSellerCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty()
                    .WithMessage(ValidationMessages.required("UserId"));

            RuleFor(x => x.ShopName)
                .NotEmpty()
                    .WithMessage(ValidationMessages.required("ShopName"))
                .MaximumLength(200)
                    .WithMessage(ValidationMessages.maxLength("ShopName", 200))
                .MinimumLength(3)
                    .WithMessage(ValidationMessages.minLength("ShopName", 3));

            RuleFor(x => x.NationalCode)
                .NotEmpty()
                    .WithMessage(ValidationMessages.required("NationalCode"))
                .Length(10)
                    .WithMessage("National code must be 10 digits")
                .Matches(@"^[0-9]{10}$")
                    .WithMessage("National code must contain only digits");
        }
    }
}