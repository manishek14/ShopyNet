using Common.Application.Validation;
using FluentValidation;

namespace Shop.Application.Order.AddItem
{
    public class AddOrderItemCommandValidator : AbstractValidator<AddOrderItemCommand>
    {
        public AddOrderItemCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty()
                    .WithMessage(ValidationMessages.required("UserId"));

            RuleFor(x => x.InventoryId)
                .NotEmpty()
                    .WithMessage(ValidationMessages.required("InventoryId"));

            RuleFor(x => x.Count)
                .GreaterThan(0)
                    .WithMessage("Count must be greater than zero");

            RuleFor(x => x.Price)
                .GreaterThan(0)
                    .WithMessage("Price must be greater than zero");
        }
    }
}