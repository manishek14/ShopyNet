using Common.Query;
using System;

namespace Shop.Query.Order.DTOs
{
    public class OrderItemDto : BaseDto
    {
        public Guid OrderId { get; set; }
        public Guid InventoryId { get; set; }
        public int Count { get; set; }
        public int Price { get; set; }
        public int TotalPrice { get; set; }
    }
}