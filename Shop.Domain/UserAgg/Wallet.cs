using Common.Domain;
using Shop.Domain.UserAgg.Enums;
using System;
using System.IO;

namespace Shop.Domain.UserAgg
{
    public class Wallet : BaseEntity
    {
        private Wallet() { }

        public Wallet(
            Guid userId,
            int price,
            string description,
            WalletType type)
        {
            UserId = userId;
            Price = price;
            Description = description;
            Type = type;
            CreatedAt = DateTime.Now;
            IsFinally = false;

            Guard();
        }

        public Guid UserId { get; private set; }
        public int Price { get; private set; }
        public string Description { get; private set; } = string.Empty;
        public WalletType Type { get; private set; }
        public bool IsFinally { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? FinallyAt { get; private set; }

        public void Finally()
        {
                if (IsFinally)
                    throw new InvalidOperationException("Wallet is already finalized.");

                IsFinally = true;
                FinallyAt = DateTime.Now;
        }

        public void IncreaseBalance(int amount)
        {
            try
            {
                if (amount <= 0)
                    throw new InvalidDataException("Amount must be greater than zero!");

                if (IsFinally)
                    throw new InvalidOperationException("Cannot modify a finalized wallet!");

                Price += amount;
            }
            finally
            {
                Guard();
            }
        }

        public void DecreaseBalance(int amount)
        {
            try
            {
                if (amount <= 0)
                    throw new InvalidDataException("Amount must be greater than zero!");

                if (IsFinally)
                    throw new InvalidOperationException("Cannot modify a finalized wallet!");

                if (Price - amount < 0)
                    throw new InvalidOperationException("Insufficient balance!");

                Price -= amount;
            }
            finally
            {
                Guard();
            }
        }

        private void Guard()
        {
                if (UserId == Guid.Empty)
                    throw new NullOrEmptyDomainDataException("UserId cannot be empty!");

                if (Price < 0)
                    throw new InvalidDataException("Price cannot be negative!");

                NullOrEmptyDomainDataException.CheckString(Description, nameof(Description));

                if (!Enum.IsDefined(typeof(WalletType), Type))
                    throw new InvalidDataException("Wallet type is not valid!");
        }
    }
}