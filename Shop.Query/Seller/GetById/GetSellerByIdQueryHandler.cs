using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.Seller.DTOs;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Query.Seller.GetById
{
    internal class GetSellerByIdQueryHandler : IBaseQueryHandler<GetSellerByIdQuery, SellerDto>
    {
        private readonly ShopContext _shopContext;

        public GetSellerByIdQueryHandler(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        public async Task<SellerDto> Handle(GetSellerByIdQuery request, CancellationToken cancellationToken)
        {
            var seller = await _shopContext.Sellers
                .Include(s => s.Inventories)
                .SingleOrDefaultAsync(s => s.Id == request.Id, cancellationToken);

            return seller.Map();
        }
    }
}