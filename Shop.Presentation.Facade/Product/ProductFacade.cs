using Common.Aplication;
using MediatR;
using Shop.Application.Products.AddImage;
using Shop.Application.Products.Create;
using Shop.Application.Products.Edit;
using Shop.Application.Products.RemoveImage;
using Shop.Query.Product.DTOs;
using Shop.Query.Product.GetByFilter;
using Shop.Query.Product.GetById;
using Shop.Query.Product.GetBySlug;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shop.Presentation.Facade.Product
{
    internal class ProductFacade : IProductFacade
    {
        private readonly IMediator _mediator;

        public ProductFacade(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<OperationResult> Create(CreateProductCommand command)
            => await _mediator.Send(command);

        public async Task<OperationResult> Edit(EditProductCommand command)
            => await _mediator.Send(command);

        public async Task<OperationResult> AddImage(AddProductImageCommand command)
            => await _mediator.Send(command);

        public async Task<OperationResult> RemoveImage(RemoveProductImageCommand command)
            => await _mediator.Send(command);

        public async Task<ProductDto> GetProductById(Guid id)
            => await _mediator.Send(new GetProductByIdQuery(id));

        public async Task<ProductDto> GetProductBySlug(string slug)
            => await _mediator.Send(new GetProductBySlugQuery(slug));

        public async Task<ProductFilterData> GetProductsByFilterQuery(ProductFilterParams filterParams)
            => await _mediator.Send(new GetProductsByFilterQuery(filterParams));
    }
}