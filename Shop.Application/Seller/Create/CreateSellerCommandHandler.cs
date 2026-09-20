using Common.Aplication;
using Shop.Domain.SellerAgg;
using Shop.Domain.SellerAgg.Enums;

namespace Shop.Application.Sellers.Create
{
    public class CreateSellerCommandHandler : IBaseCommandHandler<CreateSellerCommand>
    {
        private readonly ISellerRepository _sellerRepository;

        public CreateSellerCommandHandler(ISellerRepository sellerRepository)
        {
            _sellerRepository = sellerRepository
                ?? throw new ArgumentNullException(nameof(sellerRepository));
        }

        public async Task<OperationResult> Handle(
            CreateSellerCommand request,
            CancellationToken cancellationToken)
        {
            var seller = new Domain.SellerAgg.Seller(
                request.UserId,
                request.ShopName,
                request.NationalCode,
                SellerStatus.Pending,
                new List<SellerInventory>(),
                DateTime.Now,
                DateTime.Now
            );

            await _sellerRepository.AddAsync(seller, cancellationToken);
            await _sellerRepository.SaveAsync(cancellationToken);

            return OperationResult.Success();
        }
    }
}