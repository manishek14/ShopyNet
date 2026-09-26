using Common.Application.Validation;
using FluentValidation;

namespace Shop.Application.Order.SetShippingMethod
{
    public class SetOrderShippingMethodCommandValidator
        : AbstractValidator<SetOrderShippingMethodCommand>
    {
        public SetOrderShippingMethodCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage(ValidationMessages.required("UserId"));

            RuleFor(x => x.ShippingType)
                .NotEmpty().WithMessage(ValidationMessages.required("ShippingType"))
                .MaximumLength(100).WithMessage(ValidationMessages.maxLength("ShippingType", 100));

            RuleFor(x => x.ShippingCost)
                .GreaterThanOrEqualTo(0).WithMessage("Shipping cost cannot be negative");
        }
    }
}