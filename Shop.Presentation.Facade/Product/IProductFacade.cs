using Common.Aplication;
using Shop.Application.Products.AddImage;
using Shop.Application.Products.Create;
using Shop.Application.Products.Edit;
using Shop.Application.Products.RemoveImage;
using Shop.Query.Product.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shop.Presentation.Facade.Product
{
    public interface IProductFacade
    {
        Task<OperationResult> Create(CreateProductCommand command);
        Task<OperationResult> Edit(EditProductCommand command);
        Task<OperationResult> AddImage(AddProductImageCommand command);
        Task<OperationResult> RemoveImage(RemoveProductImageCommand command);

        Task<ProductDto> GetProductById(Guid id);
        Task<ProductDto> GetProductBySlug(string slug);
        Task<ProductFilterData> GetProductsByFilterQuery(ProductFilterParams FilterParams);
    }
}