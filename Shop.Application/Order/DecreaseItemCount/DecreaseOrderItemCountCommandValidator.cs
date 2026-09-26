using Common.Application.Validation;
using FluentValidation;

namespace Shop.Application.Order.DecreaseItemCount
{
    public class DecreaseOrderItemCountCommandValidator
        : AbstractValidator<DecreaseOrderItemCountCommand>
    {
        public DecreaseOrderItemCountCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage(ValidationMessages.required("UserId"));

            RuleFor(x => x.InventoryId)
                .NotEmpty().WithMessage(ValidationMessages.required("InventoryId"));

            RuleFor(x => x.Count)
                .GreaterThan(0).WithMessage("Count must be greater than zero");
        }
    }
}