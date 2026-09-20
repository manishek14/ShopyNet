using Common.Domain;

namespace Shop.Domain.SellerAgg
{
    public class SellerInventory : BaseEntity
    {
        public SellerInventory(Guid sellerId, Guid productId, int count, int price)
        { 
            SellerId = sellerId;
            ProductId = productId;
            GuardInventory(count, price);
            Count = count;
            Price = price;
        }

        public Guid SellerId { get; internal set; }
        public Guid ProductId { get; private set; }
        public int Count { get; private set; }
        public int Price { get; private set; }

        public void GuardInventory(int count, int price)
        {
            if (count < 0)
                throw new InvalidDataException("Count cannot be negative");
            if (price < 0)
                throw new InvalidDataException("Price cannot be negative");
        }
    }
}
