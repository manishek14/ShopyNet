using Shop.Query.Category.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Query.Category.GetById
{
    public record GetCategoryByIdQuery(Guid Id) : IBaseQuery<CategoryDto>
    {
    }
}
