using Microsoft.EntityFrameworkCore;
using Shop.Domain.UserAgg;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.User.DTOs;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Query.User.GetWalletById
{
    internal class GetWalletByIdQueryHandler : IBaseQueryHandler<GetWalletByIdQuery, WalletDto>
    {
        private readonly ShopContext _shopContext;

        public GetWalletByIdQueryHandler(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        public async Task<WalletDto> Handle(GetWalletByIdQuery request, CancellationToken cancellationToken)
        {
            var wallet = await _shopContext
                .Set<Wallet>()
                .SingleOrDefaultAsync(w => w.Id == request.WalletId, cancellationToken);

            return wallet?.Map();
        }
    }
}