using Common.Query.Filter;

namespace Shop.Query.Category.DTOs
{
    public class CategoryFilterParams : BaseFilter.BaseFilterParam
    {
        public string? Title { get; set; }
        public string? Slug { get; set; }
        public Guid? ParentId { get; set; }
    }
}