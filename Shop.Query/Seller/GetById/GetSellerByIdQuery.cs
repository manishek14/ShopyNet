using Shop.Query.Seller.DTOs;
using System;

namespace Shop.Query.Seller.GetById
{
    public record GetSellerByIdQuery(Guid Id) : IBaseQuery<SellerDto>;
}