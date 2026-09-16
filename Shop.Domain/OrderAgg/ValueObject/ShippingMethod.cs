using Common.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Domain.OrderAgg.ValueObject
{
    public class ShippingMethod : BaseValueObject
    {
        public ShippingMethod(string ShippingType, int ShippingCost)
        {
            this.ShippingType = ShippingType;
            this.ShippingCost = ShippingCost;
        }

        public string ShippingType { get; private set; }
        public int ShippingCost { get; private set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return ShippingType;
            yield return ShippingCost;
        }
    }
}
