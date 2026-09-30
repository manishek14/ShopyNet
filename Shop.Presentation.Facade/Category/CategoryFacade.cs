using Common.Aplication;
using MediatR;
using Shop.Application.Category.AddChild;
using Shop.Application.Category.Create;
using Shop.Application.Category.Edit;
using Shop.Query.Category.DTOs;
using Shop.Query.Category.GetByFilter;
using Shop.Query.Category.GetByFilterQuery;
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
            => await _mediator.Send(command);

        public async Task<OperationResult> Create(CreateCategoryCommand command)
            => await _mediator.Send(command);

        public async Task<OperationResult> Edit(EditCategoryCommand command)
            => await _mediator.Send(command);

        public async Task<CategoryDto> GetCategoryById(Guid id)
            => await _mediator.Send(new GetCategoryByIdQuery(id));

        public async Task<List<CategoryWithParentDto>> GetCategoriesByParent(Guid parentId)
            => await _mediator.Send(new GetCategoryByParentQuery(parentId));

        public async Task<List<CategoryWithChildsDto>> GetCategories()
            => await _mediator.Send(new GetCategoriesListQuery());

        public async Task<CategoryFilterData> GetCategoriesByFilter(CategoryFilterParams filterParams)
            => await _mediator.Send(new GetCategoriesByFilterQuery(filterParams));
    }
}