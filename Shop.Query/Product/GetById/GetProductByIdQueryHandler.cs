using Microsoft.EntityFrameworkCore;
using Shop.Application.Caching;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.Product.DTOs;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Query.Product.GetById
{
    internal class GetProductByIdQueryHandler
        : IBaseQueryHandler<GetProductByIdQuery, ProductDto>
    {
        private readonly ShopContext _shopContext;
        private readonly ICacheService _cache;

        public GetProductByIdQueryHandler(ShopContext shopContext, ICacheService cache)
        {
            _shopContext = shopContext;
            _cache = cache;
        }

        public async Task<ProductDto> Handle(
            GetProductByIdQuery request,
            CancellationToken cancellationToken)
        {
            var cacheKey = CacheKeys.ProductById(request.Id);

            return await _cache.GetOrSetAsync(
                cacheKey,
                async () =>
                {
                    var product = await _shopContext.Products
                        .AsNoTracking()
                        .Include(p => p.Images)
                        .Include(p => p.Specifications)
                        .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

                    return product.Map();
                },
                TimeSpan.FromMinutes(30),
                cancellationToken);
        }
    }
}