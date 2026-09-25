using Microsoft.EntityFrameworkCore;
using Shop.Domain.SellerAgg;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.Seller.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Query.Seller.GetInventories
{
    internal class GetSellerInventoriesQueryHandler
        : IBaseQueryHandler<GetSellerInventoriesQuery, List<SellerInventoryDto>>
    {
        private readonly ShopContext _shopContext;

        public GetSellerInventoriesQueryHandler(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        public async Task<List<SellerInventoryDto>> Handle(
            GetSellerInventoriesQuery request,
            CancellationToken cancellationToken)
        {
            var inventories = await _shopContext
                .Set<SellerInventory>()
                .Where(si => si.SellerId == request.SellerId)
                .Select(si => new SellerInventoryDto
                {
                    Id = si.Id,
                    SellerId = si.SellerId,
                    ProductId = si.ProductId,
                    Count = si.Count,
                    Price = si.Price
                })
                .ToListAsync(cancellationToken);

            return inventories;
        }
    }
}