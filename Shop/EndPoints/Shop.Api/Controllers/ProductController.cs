using Common.Aplication;
using Common.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Products.AddImage;
using Shop.Application.Products.Create;
using Shop.Application.Products.Edit;
using Shop.Application.Products.RemoveImage;
using Shop.Presentation.Facade.Product;
using Shop.Query.Product.DTOs;
using System;
using System.Threading.Tasks;

namespace Shop.Api.Controllers
{
    public class ProductController : ApiController
    {
        private readonly IProductFacade _productFacade;

        public ProductController(IProductFacade productFacade)
        {
            _productFacade = productFacade;
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResult<ProductDto>>> GetProductById(Guid id)
        {
            var product = await _productFacade.GetProductById(id);
            if (product == null) return NotFound(new { message = "Product not found" });
            return QueryResult(product);
        }

        [HttpGet("slug/{slug}")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResult<ProductDto>>> GetProductBySlug(string slug)
        {
            var product = await _productFacade.GetProductBySlug(slug);
            if (product == null) return NotFound(new { message = "Product not found" });
            return QueryResult(product);
        }

        [HttpGet("filter")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResult<ProductFilterData>>> GetProductsByFilter(
            [FromQuery] ProductFilterParams filterParams)
        {
            var result = await _productFacade.GetProductsByFilterQuery(filterParams);
            return QueryResult(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [RequestSizeLimit(10 * 1024 * 1024)]
        public async Task<ActionResult<ApiResult>> CreateProduct(
            [FromForm] CreateProductCommand command)
        {
            var result = await _productFacade.Create(command);
            return CommandResult(result);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        [RequestSizeLimit(10 * 1024 * 1024)]
        public async Task<ActionResult<ApiResult>> EditProduct(
            Guid id, [FromForm] EditProductCommand command)
        {
            if (id != command.Id)
                return BadRequest(new { message = "Id in URL does not match Id in body" });

            var result = await _productFacade.Edit(command);
            return CommandResult(result);
        }

        [HttpPost("add-image")]
        [Authorize(Roles = "Admin")]
        [RequestSizeLimit(10 * 1024 * 1024)]
        public async Task<ActionResult<ApiResult>> AddImage(
            [FromForm] AddProductImageCommand command)
        {
            var result = await _productFacade.AddImage(command);
            return CommandResult(result);
        }

        [HttpDelete("remove-image")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiResult>> RemoveImage(
            [FromBody] RemoveProductImageCommand command)
        {
            var result = await _productFacade.RemoveImage(command);
            return CommandResult(result);
        }
    }
}