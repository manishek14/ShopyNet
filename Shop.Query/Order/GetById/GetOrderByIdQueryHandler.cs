using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.Order.DTOs;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Query.Order.GetById
{
    internal class GetOrderByIdQueryHandler : IBaseQueryHandler<GetOrderByIdQuery, OrderDto>
    {
        private readonly ShopContext _shopContext;

        public GetOrderByIdQueryHandler(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        public async Task<OrderDto> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var order = await _shopContext.Orders
                .Include(o => o.Items)
                .SingleOrDefaultAsync(o => o.Id == request.Id, cancellationToken);

            return order.Map();
        }
    }
}