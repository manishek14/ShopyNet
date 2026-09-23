using Common.Domain.ValueObject;
using Common.Query;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Query.Category.DTOs
{
    public class CategoryDto : BaseDto
    {
        public string Title { get; set; }
        public string Slug { get; set; }
        public SeoData SeoData { get; set; }
        public Guid? ParentID { get; set; }
    }
}
