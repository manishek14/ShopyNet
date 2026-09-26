using Common.Application.Validation;
using FluentValidation;

namespace Shop.Application.Order.Finally
{
    public class FinallyOrderCommandValidator : AbstractValidator<FinallyOrderCommand>
    {
        public FinallyOrderCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage(ValidationMessages.required("UserId"));
        }
    }
}