using Common.Aplication;
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
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductFacade _productFacade;

        public ProductController(IProductFacade productFacade)
        {
            _productFacade = productFacade;
        }

        // Query
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProductById(Guid id)
        {
            var product = await _productFacade.GetProductById(id);
            if (product == null) return NotFound(new { message = "Product not found" });
            return Ok(product);
        }

        [HttpGet("slug/{slug}")]
        public async Task<ActionResult<ProductDto>> GetProductBySlug(string slug)
        {
            var product = await _productFacade.GetProductBySlug(slug);
            if (product == null) return NotFound(new { message = "Product not found" });
            return Ok(product);
        }

        [HttpGet("filter")]
        public async Task<ActionResult<ProductFilterData>> GetProductsByFilter(
            [FromQuery] ProductFilterParams filterParams)
        {
            var result = await _productFacade.GetProductsByFilterQuery(filterParams);
            return Ok(result);
        }

        // Command
        [HttpPost]
        [RequestSizeLimit(10 * 1024 * 1024)] // 10MB
        public async Task<ActionResult<OperationResult>> CreateProduct(
            [FromForm] CreateProductCommand command)
        {
            var result = await _productFacade.Create(command);
            return result.Status == OperationResultStatus.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPut("{id}")]
        [RequestSizeLimit(10 * 1024 * 1024)]
        public async Task<ActionResult<OperationResult>> EditProduct(
            Guid id, [FromForm] EditProductCommand command)
        {
            if (id != command.Id)
                return BadRequest(new { message = "Id in URL does not match Id in body" });

            var result = await _productFacade.Edit(command);
            return result.Status == OperationResultStatus.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPost("add-image")]
        [RequestSizeLimit(10 * 1024 * 1024)]
        public async Task<ActionResult<OperationResult>> AddImage([FromForm] AddProductImageCommand command)
        {
            var result = await _productFacade.AddImage(command);
            return result.Status == OperationResultStatus.Success ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("remove-image")]
        public async Task<ActionResult<OperationResult>> RemoveImage([FromBody] RemoveProductImageCommand command)
        {
            var result = await _productFacade.RemoveImage(command);
            return result.Status == OperationResultStatus.Success ? Ok(result) : BadRequest(result);
        }
    }
}