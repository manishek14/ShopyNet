using Shop.Query.Product.DTOs;

namespace Shop.Query.Product.GetBySlug
{
    public record GetProductBySlugQuery(string Slug) : IBaseQuery<ProductDto>;
}