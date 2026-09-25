using Common.Query.Filter;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.Product.DTOs;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Query.Product.GetByFilter
{
    internal class GetProductsByFilterQueryHandler
        : IBaseQueryHandler<GetProductsByFilterQuery, ProductFilterData>
    {
        private readonly ShopContext _shopContext;

        public GetProductsByFilterQueryHandler(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        public async Task<ProductFilterData> Handle(
            GetProductsByFilterQuery request,
            CancellationToken cancellationToken)
        {
            var filterParams = request.FilterParams;

            var query = _shopContext.Products
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(filterParams.Title))
            {
                query = query.Where(p => p.Title.Contains(filterParams.Title));
            }

            if (!string.IsNullOrWhiteSpace(filterParams.Slug))
            {
                query = query.Where(p => p.Slug == filterParams.Slug);
            }

            if (filterParams.CategoryId.HasValue && filterParams.CategoryId != Guid.Empty)
            {
                query = query.Where(p => p.CategoryId == filterParams.CategoryId.Value);
            }

            if (filterParams.SubCategoryId.HasValue && filterParams.SubCategoryId != Guid.Empty)
            {
                query = query.Where(p => p.SubCategoryId == filterParams.SubCategoryId.Value);
            }

            if (filterParams.NestedCategoryId.HasValue && filterParams.NestedCategoryId != Guid.Empty)
            {
                query = query.Where(p => p.NestedCategoryId == filterParams.NestedCategoryId.Value);
            }

            if (filterParams.IsActive.HasValue)
            {
                query = query.Where(p => p.IsActive == filterParams.IsActive.Value);
            }

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

            var total = await query.CountAsync(cancellationToken);
            ((BaseFilter)result).GeneratePaging(total, filterParams.Limit, filterParams.PageId);

            return result;
        }
    }
}