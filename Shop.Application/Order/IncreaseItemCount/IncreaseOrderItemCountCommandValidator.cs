using Common.Application.Validation;
using FluentValidation;

namespace Shop.Application.Order.IncreaseItemCount
{
    public class IncreaseOrderItemCountCommandValidator
        : AbstractValidator<IncreaseOrderItemCountCommand>
    {
        public IncreaseOrderItemCountCommandValidator()
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