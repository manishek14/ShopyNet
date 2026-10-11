using Microsoft.EntityFrameworkCore;
using Shop.Application.Caching;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.Product.DTOs;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Query.Product.GetByFilter
{
    internal class GetProductsByFilterQueryHandler
        : IBaseQueryHandler<GetProductsByFilterQuery, ProductFilterData>
    {
        private readonly ShopContext _shopContext;
        private readonly ICacheService _cache;

        public GetProductsByFilterQueryHandler(ShopContext shopContext, ICacheService cache)
        {
            _shopContext = shopContext;
            _cache = cache;
        }

        public async Task<ProductFilterData> Handle(
            GetProductsByFilterQuery request,
            CancellationToken cancellationToken)
        {
            // ✅ ساخت Cache Key از Hash فیلترها
            var filterHash = ComputeHash(request.FilterParams);
            var cacheKey = CacheKeys.ProductsByFilter(filterHash);

            return await _cache.GetOrSetAsync(
                cacheKey,
                async () =>
                {
                    var filterParams = request.FilterParams;

                    var query = _shopContext.Products.AsQueryable();

                    if (!string.IsNullOrWhiteSpace(filterParams.Title))
                        query = query.Where(p => p.Title.Contains(filterParams.Title));

                    if (!string.IsNullOrWhiteSpace(filterParams.Slug))
                        query = query.Where(p => p.Slug == filterParams.Slug);

                    if (filterParams.CategoryId.HasValue && filterParams.CategoryId != Guid.Empty)
                        query = query.Where(p => p.CategoryId == filterParams.CategoryId.Value);

                    if (filterParams.SubCategoryId.HasValue && filterParams.SubCategoryId != Guid.Empty)
                        query = query.Where(p => p.SubCategoryId == filterParams.SubCategoryId.Value);

                    if (filterParams.NestedCategoryId.HasValue && filterParams.NestedCategoryId != Guid.Empty)
                        query = query.Where(p => p.NestedCategoryId == filterParams.NestedCategoryId.Value);

                    if (filterParams.IsActive.HasValue)
                        query = query.Where(p => p.IsActive == filterParams.IsActive.Value);

                    query = query.OrderByDescending(p => p.CreatedAt);

                    var skip = (filterParams.PageId - 1) * filterParams.Limit;
                    var products = await query
                        .Skip(skip)
                        .Take(filterParams.Limit)
                        .ToListAsync(cancellationToken);

                    var result = new ProductFilterData
                    {
                        Data = products.MapList(),
                        FilterParam = filterParams
                    };

                    result.GeneratePaging(query, filterParams.Limit, filterParams.PageId);

                    return result;
                },
                TimeSpan.FromMinutes(5),
                cancellationToken);
        }

        private static string ComputeHash(object obj)
        {
            var json = JsonSerializer.Serialize(obj);
            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(json));
            return Convert.ToBase64String(bytes).Replace("/", "_").Replace("+", "-").Replace("=", "");
        }
    }
}