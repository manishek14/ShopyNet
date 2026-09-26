using Common.Application.Validation;
using FluentValidation;

namespace Shop.Application.Order.ApplyDiscount
{
    public class ApplyOrderDiscountCommandValidator
        : AbstractValidator<ApplyOrderDiscountCommand>
    {
        public ApplyOrderDiscountCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage(ValidationMessages.required("UserId"));

            RuleFor(x => x.DiscountTitle)
                .NotEmpty().WithMessage(ValidationMessages.required("DiscountTitle"))
                .MaximumLength(200).WithMessage(ValidationMessages.maxLength("DiscountTitle", 200));

            RuleFor(x => x.DiscountAmount)
                .GreaterThan(0).WithMessage("Discount amount must be greater than zero");
        }
    }
}