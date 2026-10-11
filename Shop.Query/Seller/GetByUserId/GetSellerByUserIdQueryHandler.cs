using Microsoft.EntityFrameworkCore;
using Shop.Application.Caching;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.Seller.DTOs;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Query.Seller.GetByUserId
{
    internal class GetSellerByUserIdQueryHandler
        : IBaseQueryHandler<GetSellerByUserIdQuery, SellerDto>
    {
        private readonly ShopContext _shopContext;
        private readonly ICacheService _cache;

        public GetSellerByUserIdQueryHandler(ShopContext shopContext, ICacheService cache)
        {
            _shopContext = shopContext;
            _cache = cache;
        }

        public async Task<SellerDto> Handle(
            GetSellerByUserIdQuery request,
            CancellationToken cancellationToken)
        {
            var cacheKey = CacheKeys.SellerByUserId(request.UserId);

            return await _cache.GetOrSetAsync(
                cacheKey,
                async () =>
                {
                    var seller = await _shopContext.Sellers
                        .AsNoTracking()
                        .Include(s => s.Inventories)
                        .FirstOrDefaultAsync(s => s.UserId == request.UserId, cancellationToken);

                    return seller.Map();
                },
                TimeSpan.FromMinutes(30),
                cancellationToken);
        }
    }
}