using Common.Aplication;
using Common.Application.Validation;
using Shop.Domain.SellerAgg;

namespace Shop.Application.Sellers.Edit
{
    public class EditSellerCommandHandler : IBaseCommandHandler<EditSellerCommand>
    {
        private readonly ISellerRepository _sellerRepository;

        public EditSellerCommandHandler(ISellerRepository sellerRepository)
        {
            _sellerRepository = sellerRepository
                ?? throw new ArgumentNullException(nameof(sellerRepository));
        }

        public async Task<OperationResult> Handle(
            EditSellerCommand request,
            CancellationToken cancellationToken)
        {
            var seller = await _sellerRepository
                .GetByIdAsync(request.Id, cancellationToken);

            if (seller == null)
                return OperationResult.NotFound(ValidationMessages.NotFound);

            seller.Edit(request.ShopName, request.NationalCode);

            _sellerRepository.Update(seller);
            await _sellerRepository.SaveAsync(cancellationToken);

            return OperationResult.Success();
        }
    }
}