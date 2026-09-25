using Common.Query;
using Shop.Domain.OrderAgg.Enums;
using System;
using System.Collections.Generic;

namespace Shop.Query.Order.DTOs
{
    public class OrderDto : BaseDto
    {
        public Guid UserId { get; set; }
        public OrderStatus Status { get; set; }
        public int TotalPrice { get; set; }
        public int TotalPriceAfterDiscount { get; set; }
        public int TotalPriceWithShipping { get; set; }
        public int ItemCount { get; set; }
        public DateTime? FinallyAt { get; set; }
        public List<OrderItemDto> Items { get; set; } = new();
        public OrderDiscountDto? Discount { get; set; }
        public OrderAddressDto? Address { get; set; }
        public ShippingMethodDto? ShippingMethod { get; set; }
    }
}