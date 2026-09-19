using Common.Aplication;
using Common.Application.Validation;
using Shop.Domain.CategoryAgg;

namespace Shop.Application.Category.Edit
{
    public class EditCategoryCommandHandler : IBaseCommandHandler<EditCategoryCommand>
    {
        private readonly ICategoryRepository _categoryRepository;

        public EditCategoryCommandHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
        }

        public async Task<OperationResult> Handle(
            EditCategoryCommand request,
            CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetByIdAsync(request.Id, cancellationToken);

            if (category == null)
                return OperationResult.NotFound(ValidationMessages.NotFound);

            category.Edit(request.Title, request.Slug, request.SeoData);

            _categoryRepository.Update(category);
            await _categoryRepository.SaveAsync(cancellationToken);

            return OperationResult.Success();
        }
    }
}