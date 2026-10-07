using Common.Aplication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Shop.Application.Category.AddChild;
using Shop.Application.Category.Create;
using Shop.Application.Category.Edit;
using Shop.Presentation.Facade.Category;
using Shop.Query.Category.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableRateLimiting("ApiPolicy")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryFacade _categoryFacade;

        public CategoryController(ICategoryFacade categoryFacade)
        {
            _categoryFacade = categoryFacade;
        }

        // Query
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<CategoryDto>> GetCategoryById(Guid id)
        {
            var category = await _categoryFacade.GetCategoryById(id);
            if (category == null) return NotFound(new { message = "Category not found" });
            return Ok(category);
        }

        [HttpGet("parent/{parentId}")]
        [AllowAnonymous]
        public async Task<ActionResult<List<CategoryWithParentDto>>> GetCategoriesByParent(Guid parentId)
        {
            var categories = await _categoryFacade.GetCategoriesByParent(parentId);
            return Ok(categories);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<CategoryWithChildsDto>>> GetCategories()
        {
            var categories = await _categoryFacade.GetCategories();
            return Ok(categories);
        }

        [HttpGet("filter")]
        [AllowAnonymous]
        public async Task<ActionResult<CategoryFilterData>> GetCategoriesByFilter(
            [FromQuery] CategoryFilterParams filterParams)
        {
            var result = await _categoryFacade.GetCategoriesByFilter(filterParams);
            return Ok(result);
        }

        // Command
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<OperationResult>> CreateCategory(
            [FromBody] CreateCategoryCommand command)
        {
            var result = await _categoryFacade.Create(command);

            if (result.Status == OperationResultStatus.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<OperationResult>> EditCategory(
            Guid id, [FromBody] EditCategoryCommand command)
        {
            if (id != command.Id)
                return BadRequest(new { message = "Id in URL does not match Id in body" });

            var result = await _categoryFacade.Edit(command);

            if (result.Status == OperationResultStatus.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPost("add-child")]
        [Authorize(Roles = "Admin")]    
        public async Task<ActionResult<OperationResult>> AddChildCategory(
            [FromBody] AddChildCategoryCommand command)
        {
            var result = await _categoryFacade.AddChild(command);

            if (result.Status == OperationResultStatus.Success)
                return Ok(result);

            return BadRequest(result);
        }
    }
}