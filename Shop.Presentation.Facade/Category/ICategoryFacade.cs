using Common.Aplication;
using Shop.Application.Category.AddChild;
using Shop.Application.Category.Create;
using Shop.Application.Category.Edit;
using Shop.Query.Category.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shop.Presentation.Facade.Category
{
    public interface ICategoryFacade
    {
        Task<OperationResult> AddChild(AddChildCategoryCommand command);
        Task<OperationResult> Edit(EditCategoryCommand command);
        Task<OperationResult> Create(CreateCategoryCommand command);

        Task<CategoryDto> GetCategoryById(Guid id);
        Task<List<CategoryWithParentDto>> GetCategoriesByParent(Guid parentId);
        Task<List<CategoryWithChildsDto>> GetCategories();

        Task<CategoryFilterData> GetCategoriesByFilter(CategoryFilterParams filterParams);
    }
}