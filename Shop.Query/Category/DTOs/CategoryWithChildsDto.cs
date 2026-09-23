using Common.Domain.ValueObject;
using Common.Query;

namespace Shop.Query.Category.DTOs
{
    public class CategoryWithChildsDto : BaseDto
    {
        public string Title { get; set; }
        public string Slug { get; set; }
        public SeoData SeoData { get; set; }
        public Guid? ParentID { get; set; }
        public List<CategoryDto> Childs { get; set; }
    }
}
