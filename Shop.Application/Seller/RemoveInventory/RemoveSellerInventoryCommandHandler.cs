using Common.Aplication;
using Common.Application.Validation;
using Shop.Domain.SellerAgg;

namespace Shop.Application.Sellers.RemoveInventory
{
    public class RemoveSellerInventoryCommandHandler : IBaseCommandHandler<RemoveSellerInventoryCommand>
    {
        private readonly ISellerRepository _sellerRepository;

        public RemoveSellerInventoryCommandHandler(ISellerRepository sellerRepository)
        {
            _sellerRepository = sellerRepository
                ?? throw new ArgumentNullException(nameof(sellerRepository));
        }

        public async Task<OperationResult> Handle(
            RemoveSellerInventoryCommand request,
            CancellationToken cancellationToken)
        {
            var seller = await _sellerRepository
                .GetByIdAsync(request.SellerId, cancellationToken);

            if (seller == null)
                return OperationResult.NotFound(ValidationMessages.NotFound);

            if (!seller.Inventories.Any(i => i.ProductId == request.ProductId))
                return OperationResult.NotFound("Inventory not found!");

            seller.RemoveInventory(request.ProductId);

            _sellerRepository.Update(seller);
            await _sellerRepository.SaveAsync(cancellationToken);

            return OperationResult.Success();
        }
    }
}