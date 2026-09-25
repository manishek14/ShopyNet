using Common.Query;
using Common.Domain.ValueObject;
using System;
using System.Collections.Generic;

namespace Shop.Query.Product.DTOs
{
    public class ProductDto : BaseDto
    {
        public string Title { get; set; } = string.Empty;
        public string ImageName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Guid CategoryId { get; set; }
        public Guid SubCategoryId { get; set; }
        public Guid NestedCategoryId { get; set; }
        public string Slug { get; set; } = string.Empty;
        public SeoData SeoData { get; set; }
        public bool IsActive { get; set; }
        public List<ProductImageDto> Images { get; set; } = new();
        public List<ProductSpecificationDto> Specifications { get; set; } = new();
    }
}