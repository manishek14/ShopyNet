using Common.Aplication;
using Common.Application.Validation;
using Shop.Domain.CategoryAgg;
using Shop.Application.Caching;

namespace Shop.Application.Category.Edit
{
    public class EditCategoryCommandHandler : IBaseCommandHandler<EditCategoryCommand>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICacheService _cache;

        public EditCategoryCommandHandler(ICategoryRepository categoryRepository, ICacheService cache)
        {
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
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

            // ✅ Invalidate category caches
            await _cache.RemoveByPatternAsync(CacheKeys.CategoriesAllPattern, cancellationToken);

            return OperationResult.Success();
        }
    }
}