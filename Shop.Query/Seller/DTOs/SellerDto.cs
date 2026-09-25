using Common.Query;
using Shop.Domain.SellerAgg.Enums;
using System;
using System.Collections.Generic;

namespace Shop.Query.Seller.DTOs
{
    public class SellerDto : BaseDto
    {
        public Guid UserId { get; set; }
        public string ShopName { get; set; } = string.Empty;
        public string NationalCode { get; set; } = string.Empty;
        public SellerStatus Status { get; set; }
        public List<SellerInventoryDto> Inventories { get; set; } = new();
    }
}