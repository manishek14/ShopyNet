using Common.Query.Filter;
using Shop.Domain.SellerAgg.Enums;
using System;

namespace Shop.Query.Seller.DTOs
{
    public class SellerFilterParams : BaseFilter.BaseFilterParam
    {
        public string? ShopName { get; set; }
        public string? NationalCode { get; set; }
        public Guid? UserId { get; set; }
        public SellerStatus? Status { get; set; }
    }
}