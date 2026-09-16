using Common.Domain;
using Shop.Domain.OrderAgg.Enums;
using Shop.Domain.OrderAgg.ValueObject;
using Shop.Domain.UserAgg;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Shop.Domain.OrderAgg
{
    public class Order : BaseAggregate
    {
        // EF Core
        private Order() { }

        public Order(Guid userId)
        {
            if (userId == Guid.Empty)
                throw new NullOrEmptyDomainDataException("UserId cannot be empty.");

            UserId = userId;
            Status = OrderStatus.Pending;
            Items = new List<OrderItem>();
            Address = null;
            Discount = null;
            ShippingMethod = null;
            CreatedAt = DateTime.Now;
        }

        public Guid UserId { get; private set; }
        public OrderStatus Status { get; private set; }
        public OrderDiscount? Discount { get; private set; }
        public OrderAddress? Address { get; private set; }
        public ShippingMethod? ShippingMethod { get; private set; }
        public List<OrderItem> Items { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? FinallyAt { get; private set; }

        public int TotalPrice => Items.Sum(s => s.TotalPrice);
        public int ItemCount => Items.Count;
        public int TotalPriceAfterDiscount => Discount != null
            ? TotalPrice - Discount.DiscountAmount
            : TotalPrice;
        public int TotalPriceWithShipping => TotalPriceAfterDiscount + (ShippingMethod?.ShippingCost ?? 0);

        public void AddItem(OrderItem item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            if (Status != OrderStatus.Pending)
                throw new InvalidOperationException("Cannot add items to a non-pending order.");

            var existingItem = Items.FirstOrDefault(i => i.InventoryId == item.InventoryId);
            if (existingItem != null)
            {
                existingItem.IncreaseCount(item.Count);
                return;
            }

            item.OrderId = Id;
            Items.Add(item);
        }

        public void RemoveItem(Guid inventoryId)
        {
            if (Status != OrderStatus.Pending)
                throw new InvalidOperationException("Cannot remove items from a non-pending order.");

            var item = Items.FirstOrDefault(i => i.InventoryId == inventoryId);
            if (item == null)
                throw new KeyNotFoundException($"Item with InventoryId {inventoryId} not found.");

            Items.Remove(item);
        }

        public void IncreaseItemCount(Guid inventoryId, int count)
        {
            if (Status != OrderStatus.Pending)
                throw new InvalidOperationException("Cannot modify a non-pending order.");

            var item = Items.FirstOrDefault(i => i.InventoryId == inventoryId);
            if (item == null)
                throw new KeyNotFoundException($"Item with InventoryId {inventoryId} not found.");

            item.IncreaseCount(count);
        }

        public void DecreaseItemCount(Guid inventoryId, int count)
        {
            if (Status != OrderStatus.Pending)
                throw new InvalidOperationException("Cannot modify a non-pending order.");

            var item = Items.FirstOrDefault(i => i.InventoryId == inventoryId);
            if (item == null)
                throw new KeyNotFoundException($"Item with InventoryId {inventoryId} not found.");

            if (item.Count - count < 1)
                throw new InvalidOperationException("Count cannot be less than 1.");

            item.DecreaseCount(count);
        }

        public void SetAddress(OrderAddress address)
        {
            if (address == null)
                throw new ArgumentNullException(nameof(address));

            if (Status != OrderStatus.Pending)
                throw new InvalidOperationException("Cannot set address for a non-pending order.");

            address.OrderId = Id;
            Address = address;
        }

        public void SetShippingMethod(ShippingMethod shippingMethod)
        {
            if (shippingMethod == null)
                throw new ArgumentNullException(nameof(shippingMethod));

            if (Status != OrderStatus.Pending)
                throw new InvalidOperationException("Cannot set shipping method for a non-pending order.");

            ShippingMethod = shippingMethod;
        }

        public void ApplyDiscount(OrderDiscount discount)
        {
            if (discount == null)
                throw new ArgumentNullException(nameof(discount));

            if (Status != OrderStatus.Pending)
                throw new InvalidOperationException("Cannot apply discount to a non-pending order.");

            if (discount.DiscountAmount > TotalPrice)
                throw new InvalidOperationException("Discount amount cannot exceed total price.");

            Discount = discount;
        }

        public void RemoveDiscount()
        {
            if (Status != OrderStatus.Pending)
                throw new InvalidOperationException("Cannot remove discount from a non-pending order.");

            Discount = null;
        }

        public void Finally()
        {
            if (Items.Count == 0)
                throw new InvalidOperationException("Cannot finalize an empty order.");

            if (Address == null)
                throw new InvalidOperationException("Address is required to finalize the order.");

            if (ShippingMethod == null)
                throw new InvalidOperationException("Shipping method is required to finalize the order.");

            if (Status != OrderStatus.Pending)
                throw new InvalidOperationException("Only pending orders can be finalized.");

            Status = OrderStatus.Finally;
            FinallyAt = DateTime.Now;

            //  Event
            // AddDomainEvent(new OrderFinalized(Id, UserId, TotalPriceWithShipping));
        }

        public void Send()
        {
            if (Status != OrderStatus.Finally)
                throw new InvalidOperationException("Only finalized orders can be sent.");

            Status = OrderStatus.Sent;
        }

        public void Cancel()
        {
            if (Status == OrderStatus.Sent)
                throw new InvalidOperationException("Cannot cancel a sent order.");

            if (Status == OrderStatus.Cancelled)
                throw new InvalidOperationException("Order is already cancelled.");

            Status = OrderStatus.Cancelled;
        }

        public void Return()
        {
            if (Status != OrderStatus.Sent)
                throw new InvalidOperationException("Only sent orders can be returned.");

            Status = OrderStatus.Returned;
        }

        public bool IsEmpty()
        {
            return Items.Count == 0;
        }
    }
}