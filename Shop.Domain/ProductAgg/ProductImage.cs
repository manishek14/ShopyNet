using Common.Domain;
using Shop.Domain.UserAgg;
using System;

namespace Shop.Domain.ProductAgg
{
    public class ProductImage : BaseEntity
    {
        private ProductImage() { }

        public ProductImage(Guid productId, string imageName, string sequence)
        {
            if (productId == Guid.Empty)
                throw new ArgumentException("ProductId cannot be empty!", nameof(productId));

            NullOrEmptyDomainDataException.CheckString(imageName, nameof(imageName));
            NullOrEmptyDomainDataException.CheckString(sequence, nameof(sequence));

            ProductId = productId;
            ImageName = imageName;
            Sequence = sequence;
        }

        public Guid ProductId { get; internal set; }
        public string ImageName { get; private set; } = string.Empty;
        public string Sequence { get; private set; } = string.Empty;
    }
}