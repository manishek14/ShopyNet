using Common.Aplication;
using Common.Application.Validation;
using Shop.Domain.SellerAgg;

namespace Shop.Application.Sellers.ChangeStatus
{
    public class ChangeSellerStatusCommandHandler : IBaseCommandHandler<ChangeSellerStatusCommand>
    {
        private readonly ISellerRepository _sellerRepository;

        public ChangeSellerStatusCommandHandler(ISellerRepository sellerRepository)
        {
            _sellerRepository = sellerRepository
                ?? throw new ArgumentNullException(nameof(sellerRepository));
        }

        public async Task<OperationResult> Handle(
            ChangeSellerStatusCommand request,
            CancellationToken cancellationToken)
        {
            var seller = await _sellerRepository
                .GetByIdAsync(request.Id, cancellationToken);

            if (seller == null)
                return OperationResult.NotFound(ValidationMessages.NotFound);

            seller.ChangeStatus(request.Status);

            _sellerRepository.Update(seller);
            await _sellerRepository.SaveAsync(cancellationToken);

            return OperationResult.Success();
        }
    }
}