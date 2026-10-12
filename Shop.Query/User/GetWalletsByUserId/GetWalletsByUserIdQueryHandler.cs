using Microsoft.EntityFrameworkCore;
using Shop.Application.Caching;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.User.DTOs;
using System;
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
        private readonly ICacheService _cache;

        public GetWalletsByUserIdQueryHandler(ShopContext shopContext, ICacheService cache)
        {
            _shopContext = shopContext;
            _cache = cache;
        }

        public async Task<List<WalletDto>> Handle(
            GetWalletsByUserIdQuery request,
            CancellationToken cancellationToken)
        {
            var cacheKey = CacheKeys.UserWallets(request.UserId);

            return await _cache.GetOrSetAsync(
                cacheKey,
                async () =>
                {
                    var wallets = await _shopContext.Wallets
                        .AsNoTracking()
                        .Where(w => w.UserId == request.UserId)
                        .OrderByDescending(w => w.CreatedAt)
                        .ToListAsync(cancellationToken);

                    return wallets.MapList();
                },
                TimeSpan.FromMinutes(5),
                cancellationToken);
        }
    }
}