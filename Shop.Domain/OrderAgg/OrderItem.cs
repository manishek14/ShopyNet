using Common.Domain;
using Shop.Domain.UserAgg;
using System;
using System.IO;

namespace Shop.Domain.OrderAgg
{
    public class OrderItem : BaseEntity
    {
        private OrderItem() { }

        public OrderItem(Guid inventoryId, int count, int price)
        {
            if (inventoryId == Guid.Empty)
                throw new NullOrEmptyDomainDataException("InventoryId cannot be empty.");

            GuardCount(count);
            GuardPrice(price);

            InventoryId = inventoryId;
            Count = count;
            Price = price;
            CreatedAt = DateTime.Now;
        }

        public Guid OrderId { get; internal set; }
        public Guid InventoryId { get; private set; }
        public int Count { get; private set; }
        public int Price { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public int TotalPrice => Count * Price;

        public void IncreaseCount(int count)
        {
            GuardCount(count);
            Count += count;
        }

        public void DecreaseCount(int count)
        {
            GuardCount(count);
            if (Count - count < 1)
                throw new InvalidOperationException("Count cannot be less than 1.");
            Count -= count;
        }

        public void SetPrice(int newPrice)
        {
            GuardPrice(newPrice);
            Price = newPrice;
        }

        private static void GuardPrice(int price)
        {
            if (price <= 0)
                throw new InvalidDataException("Price must be greater than zero.");
        }

        private static void GuardCount(int count)
        {
            if (count <= 0)
                throw new InvalidDataException("Count must be greater than zero.");
        }
    }
}