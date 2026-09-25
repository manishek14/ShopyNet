using Shop.Query.Seller.DTOs;
using System;
using System.Collections.Generic;

namespace Shop.Query.Seller.GetInventories
{
    public record GetSellerInventoriesQuery(Guid SellerId)
        : IBaseQuery<List<SellerInventoryDto>>;
}