using Shop.Domain.OrderAgg;
using Shop.Query.Order.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace Shop.Query.Order
{
    internal static class OrderMapper
    {
        public static OrderDto? Map(this Domain.OrderAgg.Order? order)
        {
            if (order is null)
                return null;

            return new OrderDto
            {
                Id = order.Id,
                UserId = order.UserId,
                Status = order.Status,
                TotalPrice = order.TotalPrice,
                TotalPriceAfterDiscount = order.TotalPriceAfterDiscount,
                TotalPriceWithShipping = order.TotalPriceWithShipping,
                ItemCount = order.ItemCount,
                FinallyAt = order.FinallyAt,
                CreatedDate = order.CreatedAt,
                Items = order.Items?.Select(MapItem).ToList() ?? new List<OrderItemDto>(),
                Discount = order.Discount is null ? null : new OrderDiscountDto
                {
                    DiscountTitle = order.Discount.DiscountTitle,
                    DiscountAmount = order.Discount.DiscountAmount
                },
                Address = order.Address is null ? null : new OrderAddressDto
                {
                    OrderId = order.Address.OrderId,
                    Province = order.Address.Province,
                    City = order.Address.City,
                    PostalCode = order.Address.PostalCode,
                    MailingAddress = order.Address.MailingAddress,
                    PhoneNumber = order.Address.PhoneNumber,
                    Name = order.Address.Name,
                    Family = order.Address.Family,
                    NationalCode = order.Address.NationalCode
                },
                ShippingMethod = order.ShippingMethod is null ? null : new ShippingMethodDto
                {
                    ShippingType = order.ShippingMethod.ShippingType,
                    ShippingCost = order.ShippingMethod.ShippingCost
                }
            };
        }

        private static OrderItemDto MapItem(OrderItem item)
        {
            return new OrderItemDto
            {
                Id = item.Id,
                OrderId = item.OrderId,
                InventoryId = item.InventoryId,
                Count = item.Count,
                Price = item.Price,
                TotalPrice = item.TotalPrice,
                CreatedDate = item.CreatedAt
            };
        }

        public static List<OrderDto> MapList(this List<Domain.OrderAgg.Order>? orders)
        {
            var result = new List<OrderDto>();
            if (orders == null)
                return result;

            foreach (var order in orders)
            {
                result.Add(new OrderDto
                {
                    Id = order.Id,
                    UserId = order.UserId,
                    Status = order.Status,
                    TotalPrice = order.TotalPrice,
                    TotalPriceAfterDiscount = order.TotalPriceAfterDiscount,
                    TotalPriceWithShipping = order.TotalPriceWithShipping,
                    ItemCount = order.ItemCount,
                    FinallyAt = order.FinallyAt,
                    CreatedDate = order.CreatedAt,
                    Items = new List<OrderItemDto>(), 
                    Discount = null,
                    Address = null,
                    ShippingMethod = null
                });
            }

            return result;
        }
    }
}