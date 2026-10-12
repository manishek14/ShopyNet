using Microsoft.EntityFrameworkCore;
using Shop.Application.Caching;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.Order.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Query.Order.GetByUserId
{
    internal class GetOrderByUserIdQueryHandler
        : IBaseQueryHandler<GetOrderByUserIdQuery, List<OrderDto>>
    {
        private readonly ShopContext _shopContext;
        private readonly ICacheService _cache;

        public GetOrderByUserIdQueryHandler(ShopContext shopContext, ICacheService cache)
        {
            _shopContext = shopContext;
            _cache = cache;
        }

        public async Task<List<OrderDto>> Handle(
            GetOrderByUserIdQuery request,
            CancellationToken cancellationToken)
        {
            var cacheKey = CacheKeys.OrdersByUserId(request.UserId);

            return await _cache.GetOrSetAsync(
                cacheKey,
                async () =>
                {
                    var orders = await _shopContext.Orders
                        .AsNoTracking()
                        .Include(o => o.Items)
                        .Where(o => o.UserId == request.UserId)
                        .OrderByDescending(o => o.CreatedAt)
                        .ToListAsync(cancellationToken);

                    return orders.MapList();
                },
                TimeSpan.FromMinutes(5),
                cancellationToken);
        }
    }
}