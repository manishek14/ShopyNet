using Common.Application.Validation;
using FluentValidation;

namespace Shop.Application.Order.RemoveDiscount
{
    public class RemoveOrderDiscountCommandValidator
        : AbstractValidator<RemoveOrderDiscountCommand>
    {
        public RemoveOrderDiscountCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage(ValidationMessages.required("UserId"));
        }
    }
}