using Common.Aplication;
using Common.Application.Validation;
using Shop.Domain.SellerAgg;

namespace Shop.Application.Sellers.EditInventory
{
    public class EditSellerInventoryCommandHandler : IBaseCommandHandler<EditSellerInventoryCommand>
    {
        private readonly ISellerRepository _sellerRepository;

        public EditSellerInventoryCommandHandler(ISellerRepository sellerRepository)
        {
            _sellerRepository = sellerRepository
                ?? throw new ArgumentNullException(nameof(sellerRepository));
        }

        public async Task<OperationResult> Handle(
            EditSellerInventoryCommand request,
            CancellationToken cancellationToken)
        {
            var seller = await _sellerRepository
                .GetByIdAsync(request.SellerId, cancellationToken);

            if (seller == null)
                return OperationResult.NotFound(ValidationMessages.NotFound);

            seller.EditInventory(
                request.ProductId,
                request.Count,
                request.Price
            );

            _sellerRepository.Update(seller);
            await _sellerRepository.SaveAsync(cancellationToken);

            return OperationResult.Success();
        }
    }
}