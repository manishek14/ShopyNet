using Shop.Query.Category.DTOs;
using System.Collections.Generic;

namespace Shop.Query.Category.GetList
{
    public record GetCategoriesListQuery() : IBaseQuery<List<CategoryWithChildsDto>>;
}
