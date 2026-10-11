using Microsoft.EntityFrameworkCore;
using Shop.Application.Caching;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.Category.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Query.Category.GetList
{
    internal class GetCategoriesListQueryHandler
        : IBaseQueryHandler<GetCategoriesListQuery, List<CategoryWithChildsDto>>
    {
        private readonly ShopContext _shopContext;
        private readonly ICacheService _cache;

        public GetCategoriesListQueryHandler(ShopContext shopContext, ICacheService cache)
        {
            _shopContext = shopContext;
            _cache = cache;
        }

        public async Task<List<CategoryWithChildsDto>> Handle(
            GetCategoriesListQuery request,
            CancellationToken cancellationToken)
        {
            var cacheKey = CacheKeys.CategoriesList;

            return await _cache.GetOrSetAsync(
                cacheKey,
                async () =>
                {
                    var categories = await _shopContext.Categories
                        .AsNoTracking()
                        .Where(c => c.ParentId == null)
                        .ToListAsync(cancellationToken);

                    return CategoryMapper.SubMap(categories);
                },
                TimeSpan.FromMinutes(60),
                cancellationToken);
        }
    }
}