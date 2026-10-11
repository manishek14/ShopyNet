using Microsoft.EntityFrameworkCore;
using Shop.Application.Caching;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.Product.DTOs;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Query.Product.GetBySlug
{
    internal class GetProductBySlugQueryHandler
        : IBaseQueryHandler<GetProductBySlugQuery, ProductDto>
    {
        private readonly ShopContext _shopContext;
        private readonly ICacheService _cache;

        public GetProductBySlugQueryHandler(ShopContext shopContext, ICacheService cache)
        {
            _shopContext = shopContext;
            _cache = cache;
        }

        public async Task<ProductDto> Handle(
            GetProductBySlugQuery request,
            CancellationToken cancellationToken)
        {
            var cacheKey = CacheKeys.ProductBySlug(request.Slug);

            return await _cache.GetOrSetAsync(
                cacheKey,
                async () =>
                {
                    var product = await _shopContext.Products
                        .AsNoTracking()
                        .Include(p => p.Images)
                        .Include(p => p.Specifications)
                        .FirstOrDefaultAsync(p => p.Slug == request.Slug, cancellationToken);

                    return product.Map();
                },
                TimeSpan.FromMinutes(30),
                cancellationToken);
        }
    }
}