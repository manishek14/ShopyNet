using Common.Domain;

namespace Shop.Domain.ProductAgg
{
    public class ProductImage : BaseEntity
    {
        public ProductImage(Guid productId, string imageName, string sequence)
        {
            ProductId = productId;
            GuardAgainstNullOrEmpty();
            ImageName = imageName;
            Sequence = sequence;
        }

        public Guid ProductId { get; internal set; }
        public string ImageName { get; private set; }
        public string Sequence { get; private set; }

        public void GuardAgainstNullOrEmpty()
        {
            if (string.IsNullOrEmpty(ImageName))
                throw new ArgumentException("ImageName cannot be null or empty!");
            if (string.IsNullOrEmpty(Sequence))
                throw new ArgumentException("Sequence cannot be null or empty!");
        }
    }
}
