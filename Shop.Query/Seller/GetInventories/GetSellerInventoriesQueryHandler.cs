using Microsoft.EntityFrameworkCore;
using Shop.Application.Caching;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.Seller.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Query.Seller.GetInventories
{
    internal class GetSellerInventoriesQueryHandler
        : IBaseQueryHandler<GetSellerInventoriesQuery, List<SellerInventoryDto>>
    {
        private readonly ShopContext _shopContext;
        private readonly ICacheService _cache;

        public GetSellerInventoriesQueryHandler(ShopContext shopContext, ICacheService cache)
        {
            _shopContext = shopContext;
            _cache = cache;
        }

        public async Task<List<SellerInventoryDto>> Handle(
            GetSellerInventoriesQuery request,
            CancellationToken cancellationToken)
        {
            var cacheKey = CacheKeys.SellerInventories(request.SellerId);

            return await _cache.GetOrSetAsync(
                cacheKey,
                async () =>
                {
                    var inventories = await _shopContext.SellerInventories
                        .AsNoTracking()
                        .Where(si => si.SellerId == request.SellerId)
                        .Select(si => new SellerInventoryDto
                        {
                            Id = si.Id,
                            SellerId = si.SellerId,
                            ProductId = si.ProductId,
                            Count = si.Count,
                            Price = si.Price
                        })
                        .ToListAsync(cancellationToken);

                    return inventories;
                },
                TimeSpan.FromMinutes(5),
                cancellationToken);
        }
    }
}