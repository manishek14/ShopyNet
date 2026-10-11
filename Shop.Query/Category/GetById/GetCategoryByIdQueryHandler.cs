using Microsoft.EntityFrameworkCore;
using Shop.Application.Caching;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.Category.DTOs;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Query.Category.GetById
{
    internal class GetCategoryByIdQueryHandler
        : IBaseQueryHandler<GetCategoryByIdQuery, CategoryDto>
    {
        private readonly ShopContext _shopContext;
        private readonly ICacheService _cache;

        public GetCategoryByIdQueryHandler(ShopContext shopContext, ICacheService cache)
        {
            _shopContext = shopContext;
            _cache = cache;
        }

        public async Task<CategoryDto> Handle(
            GetCategoryByIdQuery request,
            CancellationToken cancellationToken)
        {
            var cacheKey = CacheKeys.CategoryById(request.Id);

            return await _cache.GetOrSetAsync(
                cacheKey,
                async () =>
                {
                    var category = await _shopContext.Categories
                        .AsNoTracking()
                        .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

                    return category.Map();
                },
                TimeSpan.FromMinutes(60),
                cancellationToken);
        }
    }
}