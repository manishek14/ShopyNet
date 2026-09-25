using Common.Query;

namespace Shop.Query.Product.DTOs
{
    public class ProductSpecificationDto : BaseDto
    {
        public Guid ProductId { get; set; }
        public string Key { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
    }
}