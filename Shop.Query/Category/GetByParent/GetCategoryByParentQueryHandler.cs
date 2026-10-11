using Microsoft.EntityFrameworkCore;
using Shop.Application.Caching;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.Category.DTOs;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Query.Category.GetByParent
{
    internal class GetCategoryByParentQueryHandler
        : IBaseQueryHandler<GetCategoryByParentQuery, List<CategoryWithParentDto>>
    {
        private readonly ShopContext _shopContext;
        private readonly ICacheService _cache;

        public GetCategoryByParentQueryHandler(ShopContext shopContext, ICacheService cache)
        {
            _shopContext = shopContext;
            _cache = cache;
        }

        public async Task<List<CategoryWithParentDto>> Handle(
            GetCategoryByParentQuery request,
            CancellationToken cancellationToken)
        {
            var cacheKey = CacheKeys.CategoriesByParent(request.ParentId);

            return await _cache.GetOrSetAsync(
                cacheKey,
                async () =>
                {
                    var categories = await _shopContext.Categories
                        .AsNoTracking()
                        .Where(c => c.ParentId == request.ParentId)
                        .ToListAsync(cancellationToken);

                    return CategoryMapper.SubParentMap(categories);
                },
                TimeSpan.FromMinutes(60),
                cancellationToken);
        }
    }
}