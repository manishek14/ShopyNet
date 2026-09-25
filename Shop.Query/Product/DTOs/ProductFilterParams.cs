using Common.Query.Filter;
using System;

namespace Shop.Query.Product.DTOs
{
    public class ProductFilterParams : BaseFilter.BaseFilterParam
    {
        public string? Title { get; set; }
        public string? Slug { get; set; }
        public Guid? CategoryId { get; set; }
        public Guid? SubCategoryId { get; set; }
        public Guid? NestedCategoryId { get; set; }
        public int? MinPrice { get; set; }
        public int? MaxPrice { get; set; }
        public bool? IsActive { get; set; }
    }
}