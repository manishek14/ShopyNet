using Common.Aplication;
using MediatR;
using Shop.Application.Category.AddChild;
using Shop.Application.Category.Create;
using Shop.Application.Category.Edit;
using Shop.Query.Category.DTOs;
using Shop.Query.Category.GetById;
using Shop.Query.Category.GetByParent;
using Shop.Query.Category.GetList;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shop.Presentation.Facade.Category
{
    internal class CategoryFacade : ICategoryFacade
    {
        private readonly IMediator _mediator;

        public CategoryFacade(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<OperationResult> AddChild(AddChildCategoryCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> Create(CreateCategoryCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> Edit(EditCategoryCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<List<CategoryWithChildsDto>> GetCategories()
        {
            return await _mediator.Send(new GetCategoriesListQuery());
        }

        public async Task<List<CategoryWithParentDto>> GetCategoriesByParent(Guid parentId)
        {
            return await _mediator.Send(new GetCategoryByParentQuery(parentId));
        }

        public async Task<CategoryDto> GetCategoryById(Guid id)
        {
            return await _mediator.Send(new GetCategoryByIdQuery(id));
        }
    }
}