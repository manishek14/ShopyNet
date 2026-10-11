using Microsoft.EntityFrameworkCore;
using Shop.Application.Caching;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.Seller.DTOs;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Query.Seller.GetById
{
    internal class GetSellerByIdQueryHandler
        : IBaseQueryHandler<GetSellerByIdQuery, SellerDto>
    {
        private readonly ShopContext _shopContext;
        private readonly ICacheService _cache;

        public GetSellerByIdQueryHandler(ShopContext shopContext, ICacheService cache)
        {
            _shopContext = shopContext;
            _cache = cache;
        }

        public async Task<SellerDto> Handle(
            GetSellerByIdQuery request,
            CancellationToken cancellationToken)
        {
            var cacheKey = CacheKeys.SellerById(request.Id);

            return await _cache.GetOrSetAsync(
                cacheKey,
                async () =>
                {
                    var seller = await _shopContext.Sellers
                        .AsNoTracking()
                        .Include(s => s.Inventories)
                        .FirstOrDefaultAsync(s => s.Id == request.Id, cancellationToken);

                    return seller.Map();
                },
                TimeSpan.FromMinutes(30),
                cancellationToken);
        }
    }
}