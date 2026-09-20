using Common.Application.Validation;
using FluentValidation;

namespace Shop.Application.Sellers.EditInventory
{
    public class EditSellerInventoryCommandValidator : AbstractValidator<EditSellerInventoryCommand>
    {
        public EditSellerInventoryCommandValidator()
        {
            RuleFor(x => x.SellerId)
                .NotEmpty()
                    .WithMessage(ValidationMessages.required("SellerId"));

            RuleFor(x => x.ProductId)
                .NotEmpty()
                    .WithMessage(ValidationMessages.required("ProductId"));

            RuleFor(x => x.Count)
                .GreaterThanOrEqualTo(0)
                    .WithMessage("Count cannot be negative");

            RuleFor(x => x.Price)
                .GreaterThan(0)
                    .WithMessage("Price must be greater than zero");
        }
    }
}