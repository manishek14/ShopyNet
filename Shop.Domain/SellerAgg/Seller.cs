using Common.Domain;
using Shop.Domain.SellerAgg.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Domain.SellerAgg
{
    public class Seller : BaseAggregate
    {
        public Seller(Guid userId, string shopName, string nationalCode, SellerStatus status, List<SellerInventory> inventories, DateTime createdAt, DateTime updatedAt)
        {
            UserId = userId;
            Guard(shopName, nationalCode);
            ShopName = shopName;
            NationalCode = nationalCode;
            Status = status;
            Inventories = inventories;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }
        // EF
        private Seller() { } 
        public Guid UserId { get; private set; }
        public string ShopName { get; private set; }
        public string NationalCode { get; private set; }
        public SellerStatus Status { get; private set; }
        public List<SellerInventory> Inventories { get; set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        public void ChangeStatus(SellerStatus status)
        {
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
            Inventories.Add(inventory);
            UpdatedAt = DateTime.Now;
        }

        public void RemoveInventory(SellerInventory inventory)
        {
            if (inventory == null)
                throw new ArgumentNullException(nameof(inventory));
            Inventories.Remove(inventory);
            UpdatedAt = DateTime.Now;
        }

        public void EditInventory(SellerInventory inventory, int count, int price)
        {
            var CurrentInventory = Inventories.Find(i => i.ProductId == inventory.ProductId);
            if (inventory == null)
                throw new ArgumentNullException(nameof(inventory));
            Inventories.Remove(CurrentInventory);
            Inventories.Add(inventory);
            UpdatedAt = DateTime.Now;
        }

        public void Guard(string shopName, string nationalCode)
        {
            if (string.IsNullOrWhiteSpace(shopName))
                throw new InvalidDataException("Shop name cannot be empty.");
            if (string.IsNullOrWhiteSpace(nationalCode))
                throw new InvalidDataException("National code cannot be empty.");
        }
    }
}
