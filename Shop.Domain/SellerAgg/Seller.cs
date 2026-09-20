using Common.Domain;
using Shop.Domain.SellerAgg.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Shop.Domain.SellerAgg
{
    public class Seller : BaseAggregate
    {
        private Seller() { }

        public Seller(
            Guid userId,
            string shopName,
            string nationalCode)
        {
            if (userId == Guid.Empty)
                throw new ArgumentException("UserId cannot be empty.", nameof(userId));

            Guard(shopName, nationalCode);

            UserId = userId;
            ShopName = shopName;
            NationalCode = nationalCode;
            Status = SellerStatus.Pending;
            Inventories = new List<SellerInventory>();
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }

        public Guid UserId { get; private set; }
        public string ShopName { get; private set; } = string.Empty;
        public string NationalCode { get; private set; } = string.Empty;
        public SellerStatus Status { get; private set; }
        public List<SellerInventory> Inventories { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        public void ChangeStatus(SellerStatus status)
        {
            if (!Enum.IsDefined(typeof(SellerStatus), status))
                throw new InvalidDataException("Invalid seller status.");

            if (Status == SellerStatus.Rejected && status == SellerStatus.Accepted)
                throw new InvalidOperationException("Cannot accept a rejected seller.");

            Status = status;
            UpdatedAt = DateTime.Now;
        }

        public void Edit(string shopName, string nationalCode)
        {
            Guard(shopName, nationalCode);

            ShopName = shopName;
            NationalCode = nationalCode;
            UpdatedAt = DateTime.Now;
        }

        public void AddInventory(SellerInventory inventory)
        {
            if (inventory == null)
                throw new ArgumentNullException(nameof(inventory));

            if (Inventories.Any(i => i.ProductId == inventory.ProductId))
                throw new InvalidOperationException(
                    $"Product with ID {inventory.ProductId} already exists in inventory.");

            inventory.SellerId = Id;
            Inventories.Add(inventory);
            UpdatedAt = DateTime.Now;
        }

        public void RemoveInventory(Guid productId)
        {
            if (productId == Guid.Empty)
                throw new ArgumentException("ProductId cannot be empty.", nameof(productId));

            var inventory = Inventories.FirstOrDefault(i => i.ProductId == productId);

            if (inventory == null)
                throw new KeyNotFoundException(
                    $"Inventory with ProductId {productId} not found.");

            Inventories.Remove(inventory);
            UpdatedAt = DateTime.Now;
        }

        public void EditInventory(Guid productId, int count, int price)
        {
            if (productId == Guid.Empty)
                throw new ArgumentException("ProductId cannot be empty.", nameof(productId));

            var inventory = Inventories.FirstOrDefault(i => i.ProductId == productId);

            if (inventory == null)
                throw new KeyNotFoundException(
                    $"Inventory with ProductId {productId} not found.");

            inventory.Edit(count, price);
            UpdatedAt = DateTime.Now;
        }

        private static void Guard(string shopName, string nationalCode)
        {
            if (string.IsNullOrWhiteSpace(shopName))
                throw new InvalidDataException("Shop name cannot be empty.");

            if (string.IsNullOrWhiteSpace(nationalCode))
                throw new InvalidDataException("National code cannot be empty.");

            if (nationalCode.Length != 10)
                throw new InvalidDataException("National code must be 10 digits.");

            if (!nationalCode.All(char.IsDigit))
                throw new InvalidDataException("National code must contain only digits.");
        }
    }
}