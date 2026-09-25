using Shop.Query.Seller.DTOs;
using System;

namespace Shop.Query.Seller.GetByUserId
{
    public record GetSellerByUserIdQuery(Guid UserId) : IBaseQuery<SellerDto>;
}