using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.Order.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Query.Order.GetByUserId
{
    internal class GetOrderByUserIdQueryHandler
        : IBaseQueryHandler<GetOrderByUserIdQuery, List<OrderDto>>
    {
        private readonly ShopContext _shopContext;

        public GetOrderByUserIdQueryHandler(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        public async Task<List<OrderDto>> Handle(
            GetOrderByUserIdQuery request,
            CancellationToken cancellationToken)
        {
            var orders = await _shopContext.Orders
                .Include(o => o.Items)
                .Where(o => o.UserId == request.UserId)
                .OrderByDescending(o => o.CreatedAt)
                .ToListAsync(cancellationToken);

            return orders.MapList();
        }
    }
}