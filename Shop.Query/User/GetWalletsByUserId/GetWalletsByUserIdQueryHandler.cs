using Microsoft.EntityFrameworkCore;
using Shop.Domain.UserAgg;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.User.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Query.User.GetWalletsByUserId
{
    internal class GetWalletsByUserIdQueryHandler
        : IBaseQueryHandler<GetWalletsByUserIdQuery, List<WalletDto>>
    {
        private readonly ShopContext _shopContext;

        public GetWalletsByUserIdQueryHandler(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        public async Task<List<WalletDto>> Handle(
            GetWalletsByUserIdQuery request,
            CancellationToken cancellationToken)
        {
            var walletsQueryable = _shopContext.Wallets as IQueryable<Wallet>;
            if (walletsQueryable == null) throw new InvalidOperationException("Wallets is not IQueryable<Wallet>.");

            var wallets = await walletsQueryable
                .Where(w => w.UserId == request.UserId)
                .OrderByDescending(w => w.CreatedAt)
                .ToListAsync(cancellationToken);

            return wallets.MapList();
        }
    }
}