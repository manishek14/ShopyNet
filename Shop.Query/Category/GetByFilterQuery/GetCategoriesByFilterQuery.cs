using Shop.Query.Category.DTOs;

namespace Shop.Query.Category.GetByFilterQuery
{
    public record GetCategoriesByFilterQuery(CategoryFilterParams FilterParams)
        : IBaseQuery<CategoryFilterData>;
}