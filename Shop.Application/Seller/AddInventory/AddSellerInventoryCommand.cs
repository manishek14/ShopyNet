using Common.Aplication;
using Common.Application;
using Common.Application.Validation;
using FluentValidation;
using Shop.Domain.SellerAgg;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Application.Sellers.AddInventory
{
    public record AddSellerInventoryCommand(
        Guid SellerId,
        Guid ProductId,
        int Count,
        int Price
    ) : IBaseCommand;

    public class AddSellerInventoryCommandHandler : IBaseCommandHandler<AddSellerInventoryCommand>
    {
        private readonly ISellerRepository _sellerRepository;

        public AddSellerInventoryCommandHandler(ISellerRepository sellerRepository)
        {
            _sellerRepository = sellerRepository
                ?? throw new ArgumentNullException(nameof(sellerRepository));
        }

        public async Task<OperationResult> Handle(
            AddSellerInventoryCommand request,
            CancellationToken cancellationToken)
        {
            var seller = await _sellerRepository
                .GetByIdAsync(request.SellerId, cancellationToken);

            if (seller == null)
                return OperationResult.NotFound(ValidationMessages.NotFound);

            if (seller.Inventories.Any(i => i.ProductId == request.ProductId))
                return OperationResult.Error("Product already exists in inventory!");

            var inventory = new SellerInventory(
                seller.Id,
                request.ProductId,
                request.Count,
                request.Price
            );

            seller.AddInventory(inventory);

            _sellerRepository.Update(seller);
            await _sellerRepository.SaveAsync(cancellationToken);

            return OperationResult.Success();
        }
    }

    public class AddSellerInventoryCommandValidator : AbstractValidator<AddSellerInventoryCommand>
    {
        public AddSellerInventoryCommandValidator()
        {
            RuleFor(x => x.SellerId)
                .NotEmpty()
                    .WithMessage(ValidationMessages.required("SellerId"));

            RuleFor(x => x.ProductId)
                .NotEmpty()
                    .WithMessage(ValidationMessages.required("ProductId"));

            RuleFor(x => x.Count)
                .GreaterThan(0)
                    .WithMessage("Count must be greater than zero!");

            RuleFor(x => x.Price)
                .GreaterThan(0)
                    .WithMessage("Price must be greater than zero!");
        }
    }
}