using Common.Domain;
using System;
using System.IO;

namespace Shop.Domain.SellerAgg
{
    public class SellerInventory : BaseEntity
    {
        private SellerInventory() { }

        public SellerInventory(
            Guid sellerId,
            Guid productId,
            int count,
            int price)
        {
            if (sellerId == Guid.Empty)
                throw new ArgumentException("SellerId cannot be empty.", nameof(sellerId));

            if (productId == Guid.Empty)
                throw new ArgumentException("ProductId cannot be empty.", nameof(productId));

            Guard(count, price);

            SellerId = sellerId;
            ProductId = productId;
            Count = count;
            Price = price;
        }

        public Guid SellerId { get; internal set; }
        public Guid ProductId { get; private set; }
        public int Count { get; private set; }
        public int Price { get; private set; }

        public void Edit(int count, int price)
        {
            Guard(count, price);

            Count = count;
            Price = price;
        }

        public void IncreaseCount(int amount)
        {
            if (amount <= 0)
                throw new InvalidDataException("Amount must be greater than zero.");

            Count += amount;
        }

        public void DecreaseCount(int amount)
        {
            if (amount <= 0)
                throw new InvalidDataException("Amount must be greater than zero.");

            if (Count - amount < 0)
                throw new InvalidOperationException("Insufficient inventory count.");

            Count -= amount;
        }

        public void ChangePrice(int newPrice)
        {
            if (newPrice < 0)
                throw new InvalidDataException("Price cannot be negative.");

            Price = newPrice;
        }

        private static void Guard(int count, int price)
        {
            if (count < 0)
                throw new InvalidDataException("Count cannot be negative.");

            if (price < 0)
                throw new InvalidDataException("Price cannot be negative.");
        }
    }
}