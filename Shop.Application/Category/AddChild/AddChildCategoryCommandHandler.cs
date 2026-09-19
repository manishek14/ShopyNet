using Common.Aplication;
using Common.Application.Validation;
using Shop.Domain.CategoryAgg;

namespace Shop.Application.Category.AddChild
{
    public class AddChildCategoryCommandHandler : IBaseCommandHandler<AddChildCategoryCommand>
    {
        private readonly ICategoryRepository _categoryRepository;

        public AddChildCategoryCommandHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository
                ?? throw new ArgumentNullException(nameof(categoryRepository));
        }

        public async Task<OperationResult> Handle(
            AddChildCategoryCommand request,
            CancellationToken cancellationToken)
        {
            var parentCategory = await _categoryRepository
                .GetByIdAsync(request.ParentId, cancellationToken);

            if (parentCategory == null)
                return OperationResult.NotFound(ValidationMessages.NotFound);

            parentCategory.AddChild(request.Title, request.Slug, request.SeoData);

            _categoryRepository.Update(parentCategory);
            await _categoryRepository.SaveAsync(cancellationToken);

            return OperationResult.Success();
        }
    }
}