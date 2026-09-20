using Common.Domain;
using Shop.Domain.UserAgg;
using System;

namespace Shop.Domain.ProductAgg
{
    public class ProductSpecification : BaseEntity
    {
        private ProductSpecification() { }

        public ProductSpecification(Guid productId, string key, string value)
        {
            if (productId == Guid.Empty)
                throw new ArgumentException("ProductId cannot be empty!", nameof(productId));

            NullOrEmptyDomainDataException.CheckString(key, nameof(key));
            NullOrEmptyDomainDataException.CheckString(value, nameof(value));

            ProductId = productId;
            Key = key;
            Value = value;
        }

        public Guid ProductId { get; internal set; }
        public string Key { get; private set; } = string.Empty;
        public string Value { get; private set; } = string.Empty;
    }
}