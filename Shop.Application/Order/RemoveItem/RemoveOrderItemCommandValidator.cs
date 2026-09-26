using Common.Application.Validation;
using FluentValidation;

namespace Shop.Application.Order.RemoveItem
{
    public class RemoveOrderItemCommandValidator : AbstractValidator<RemoveOrderItemCommand>
    {
        public RemoveOrderItemCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty()
                    .WithMessage(ValidationMessages.required("UserId"));

            RuleFor(x => x.InventoryId)
                .NotEmpty()
                    .WithMessage(ValidationMessages.required("InventoryId"));
        }
    }
}