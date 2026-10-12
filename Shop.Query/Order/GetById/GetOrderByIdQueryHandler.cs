using Microsoft.EntityFrameworkCore;
using Shop.Application.Caching;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.Order.DTOs;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Query.Order.GetById
{
    internal class GetOrderByIdQueryHandler
        : IBaseQueryHandler<GetOrderByIdQuery, OrderDto>
    {
        private readonly ShopContext _shopContext;
        private readonly ICacheService _cache;

        public GetOrderByIdQueryHandler(ShopContext shopContext, ICacheService cache)
        {
            _shopContext = shopContext;
            _cache = cache;
        }

        public async Task<OrderDto> Handle(
            GetOrderByIdQuery request,
            CancellationToken cancellationToken)
        {
            var cacheKey = CacheKeys.OrderById(request.Id);

            return await _cache.GetOrSetAsync(
                cacheKey,
                async () =>
                {
                    var order = await _shopContext.Orders
                        .AsNoTracking()
                        .Include(o => o.Items)
                        .FirstOrDefaultAsync(o => o.Id == request.Id, cancellationToken);

                    return order.Map();
                },
                TimeSpan.FromMinutes(5),
                cancellationToken);
        }
    }
}