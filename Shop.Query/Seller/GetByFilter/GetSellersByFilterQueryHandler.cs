using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.Seller.DTOs;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Query.Seller.GetByFilter
{
    internal class GetSellersByFilterQueryHandler
        : IBaseQueryHandler<GetSellersByFilterQuery, SellerFilterData>
    {
        private readonly ShopContext _shopContext;

        public GetSellersByFilterQueryHandler(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        public async Task<SellerFilterData> Handle(
            GetSellersByFilterQuery request,
            CancellationToken cancellationToken)
        {
            var filterParams = request.FilterParams;

            var query = _shopContext.Sellers
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(filterParams.ShopName))
            {
                query = query.Where(s => s.ShopName.Contains(filterParams.ShopName));
            }

            if (!string.IsNullOrWhiteSpace(filterParams.NationalCode))
            {
                query = query.Where(s => s.NationalCode == filterParams.NationalCode);
            }

            if (filterParams.UserId.HasValue && filterParams.UserId != Guid.Empty)
            {
                query = query.Where(s => s.UserId == filterParams.UserId.Value);
            }

            if (filterParams.Status.HasValue)
            {
                query = query.Where(s => s.Status == filterParams.Status.Value);
            }

            query = query.OrderByDescending(s => s.CreatedAt);

            var skip = (filterParams.PageId - 1) * filterParams.Limit;
            var sellers = await query
                .Skip(skip)
                .Take(filterParams.Limit)
                .ToListAsync(cancellationToken);

            var result = new SellerFilterData
            {
                Data = sellers.MapList(),
                FilterParam = filterParams
            };

            result.GeneratePaging(query, filterParams.Limit, filterParams.PageId);

            return result;
        }
    }
}