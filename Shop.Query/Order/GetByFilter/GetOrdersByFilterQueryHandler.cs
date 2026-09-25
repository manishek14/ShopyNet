using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.Order.DTOs;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Query.Order.GetByFilter
{
    internal class GetOrdersByFilterQueryHandler
        : IBaseQueryHandler<GetOrdersByFilterQuery, OrderFilterData>
    {
        private readonly ShopContext _shopContext;

        public GetOrdersByFilterQueryHandler(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        public async Task<OrderFilterData> Handle(
            GetOrdersByFilterQuery request,
            CancellationToken cancellationToken)
        {
            var filterParams = request.FilterParams;

            var query = _shopContext.Orders
                .Include(o => o.Items)
                .AsQueryable();

            if (filterParams.UserId.HasValue && filterParams.UserId != Guid.Empty)
            {
                query = query.Where(o => o.UserId == filterParams.UserId.Value);
            }

            if (filterParams.Status.HasValue)
            {
                query = query.Where(o => o.Status == filterParams.Status.Value);
            }

            if (filterParams.FromDate.HasValue)
            {
                query = query.Where(o => o.CreatedAt >= filterParams.FromDate.Value);
            }

            if (filterParams.ToDate.HasValue)
            {
                query = query.Where(o => o.CreatedAt <= filterParams.ToDate.Value);
            }

            if (filterParams.MinPrice.HasValue)
            {
                query = query.Where(o => o.Items.Sum(i => i.Price * i.Count) >= filterParams.MinPrice.Value);
            }

            if (filterParams.MaxPrice.HasValue)
            {
                query = query.Where(o => o.Items.Sum(i => i.Price * i.Count) <= filterParams.MaxPrice.Value);
            }

            query = query.OrderByDescending(o => o.CreatedAt);

            var skip = (filterParams.PageId - 1) * filterParams.Limit;
            var orders = await query
                .Skip(skip)
                .Take(filterParams.Limit)
                .ToListAsync(cancellationToken);

            var result = new OrderFilterData
            {
                Data = orders.MapList(),
                FilterParam = filterParams
            };

            var total = await query.CountAsync(cancellationToken);
            result.GeneratePaging(total, filterParams.Limit, filterParams.PageId);

            return result;
        }
    }
}