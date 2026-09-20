using Common.Application.Validation;
using FluentValidation;

namespace Shop.Application.Sellers.RemoveInventory
{
    public class RemoveSellerInventoryCommandValidator : AbstractValidator<RemoveSellerInventoryCommand>
    {
        public RemoveSellerInventoryCommandValidator()
        {
            RuleFor(x => x.SellerId)
                .NotEmpty()
                    .WithMessage(ValidationMessages.required("SellerId"));

            RuleFor(x => x.ProductId)
                .NotEmpty()
                    .WithMessage(ValidationMessages.required("ProductId"));
        }
    }
}