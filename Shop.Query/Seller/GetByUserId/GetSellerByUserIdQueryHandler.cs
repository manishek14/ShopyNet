using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.Seller.DTOs;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Query.Seller.GetByUserId
{
    internal class GetSellerByUserIdQueryHandler
        : IBaseQueryHandler<GetSellerByUserIdQuery, SellerDto>
    {
        private readonly ShopContext _shopContext;

        public GetSellerByUserIdQueryHandler(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        public async Task<SellerDto> Handle(
            GetSellerByUserIdQuery request,
            CancellationToken cancellationToken)
        {
            var seller = await _shopContext.Sellers
                .Include(s => s.Inventories)
                .SingleOrDefaultAsync(s => s.UserId == request.UserId, cancellationToken);

            return seller.Map();
        }
    }
}