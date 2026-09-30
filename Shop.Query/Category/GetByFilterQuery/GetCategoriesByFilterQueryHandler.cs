using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.Category.DTOs;
using Shop.Query.Category.GetByFilterQuery;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Query.Category.GetByFilter
{
    internal class GetCategoriesByFilterQueryHandler
        : IBaseQueryHandler<GetCategoriesByFilterQuery, CategoryFilterData>
    {
        private readonly ShopContext _shopContext;

        public GetCategoriesByFilterQueryHandler(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        public async Task<CategoryFilterData> Handle(
            GetCategoriesByFilterQuery request,
            CancellationToken cancellationToken)
        {
            var filterParams = request.FilterParams;

            var query = _shopContext.Categories.AsQueryable();

            // فیلترها
            if (!string.IsNullOrWhiteSpace(filterParams.Title))
                query = query.Where(c => c.Title.Contains(filterParams.Title));

            if (!string.IsNullOrWhiteSpace(filterParams.Slug))
                query = query.Where(c => c.Slug == filterParams.Slug);

            if (filterParams.ParentId.HasValue && filterParams.ParentId != Guid.Empty)
                query = query.Where(c => c.ParentId == filterParams.ParentId.Value);

            // مرتب‌سازی
            query = query.OrderByDescending(c => c.CreationDate);

            // Pagination
            var skip = (filterParams.PageId - 1) * filterParams.Limit;
            var categories = await query
                .Skip(skip)
                .Take(filterParams.Limit)
                .ToListAsync(cancellationToken);

            // Result
            var result = new CategoryFilterData
            {
                Data = categories.MapList(),
                FilterParam = filterParams
            };

            result.GeneratePaging(query, filterParams.Limit, filterParams.PageId);

            return result;
        }
    }
}