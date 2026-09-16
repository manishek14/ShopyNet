using Common.Domain;
using System;
using System.IO;

namespace Shop.Domain.OrderAgg.ValueObject
{
    public class OrderDiscount : BaseValueObject
    {
        public OrderDiscount(string discountTitle, int discountAmount)
        {
            if (string.IsNullOrWhiteSpace(discountTitle))
                throw new InvalidDataException("Discount title cannot be empty.");

            if (discountAmount <= 0)
                throw new InvalidDataException("Discount amount must be greater than zero.");

            DiscountTitle = discountTitle;
            DiscountAmount = discountAmount;
        }

        public string DiscountTitle { get; private set; }
        public int DiscountAmount { get; private set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return DiscountTitle;
            yield return DiscountAmount;
        }
    }
}