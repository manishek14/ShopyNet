using Common.Domain;

namespace Shop.Domain.ProductAgg
{
    public class ProductSpecification : BaseEntity
    {
        public ProductSpecification(Guid productId, string key, string value)
        {
            ProductId = productId;
            GuardAgainstNullOrEmpty();
            Key = key;
            Value = value;
        }

        public Guid ProductId { get; internal set; }
        public string Key { get; private set; }
        public string Value { get; private set; }

        public void GuardAgainstNullOrEmpty()
        {
            if (string.IsNullOrEmpty(Key))
                throw new ArgumentException("Key cannot be null or empty!");
            if (string.IsNullOrEmpty(Value))
                throw new ArgumentException("Value cannot be null or empty!");
        }
    }
}
