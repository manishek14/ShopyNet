using Shop.Query.Product.DTOs;

namespace Shop.Query.Product.GetByFilter
{
    public record GetProductsByFilterQuery(ProductFilterParams FilterParams)
        : IBaseQuery<ProductFilterData>;
}