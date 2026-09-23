using Shop.Query.Category.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Shop.Query.Category;

namespace Shop.Query.Category.GetByParent
{
    public record GetCategoryByParentQuery(Guid ParentId) : IBaseQuery<List<CategoryWithParentDto>>;
}
