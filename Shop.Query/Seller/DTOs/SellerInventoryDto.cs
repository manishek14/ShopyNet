using Common.Query;
using System;

namespace Shop.Query.Seller.DTOs
{
    public class SellerInventoryDto : BaseDto
    {
        public Guid SellerId { get; set; }
        public Guid ProductId { get; set; }
        public int Count { get; set; }
        public int Price { get; set; }
    }
}