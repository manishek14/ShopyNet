using Common.Query;

namespace Shop.Query.Product.DTOs
{
    public class ProductImageDto : BaseDto
    {
        public Guid ProductId { get; set; }
        public string ImageName { get; set; } = string.Empty;
        public string Sequence { get; set; } = string.Empty;
    }
}