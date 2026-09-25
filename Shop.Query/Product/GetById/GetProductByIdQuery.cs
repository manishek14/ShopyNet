using Shop.Query.Product.DTOs;
using System;

namespace Shop.Query.Product.GetById
{
    public record GetProductByIdQuery(Guid Id) : IBaseQuery<ProductDto>;
}