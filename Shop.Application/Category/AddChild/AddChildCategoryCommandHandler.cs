using Common.Aplication;
using Shop.Domain.CategoryAgg;
using Shop.Application.Caching;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Application.Category.AddChild
{
    public class AddChildCategoryCommandHandler : IBaseCommandHandler<AddChildCategoryCommand>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICacheService _cache;

        public AddChildCategoryCommandHandler(ICategoryRepository categoryRepository, ICacheService cache)
        {
            _categoryRepository = categoryRepository;
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        public async Task<OperationResult> Handle(
            AddChildCategoryCommand request,
            CancellationToken cancellationToken)
        {
            var parent = await _categoryRepository.GetByIdAsync(request.ParentId, cancellationToken);
            if (parent == null)
                return OperationResult.NotFound("Parent category not found.");

            var existingCategory = await _categoryRepository.GetBySlugAsync(request.Slug, cancellationToken);
            if (existingCategory != null)
                return OperationResult.Error("Slug already exists.");

            var child = new Domain.CategoryAgg.Category(
                request.Title,
                request.Slug,
                request.SeoData
            );

            child.SetParent(request.ParentId);

            await _categoryRepository.AddAsync(child, cancellationToken);
            await _categoryRepository.SaveAsync(cancellationToken);

            // Invalidate category caches
            await _cache.RemoveByPatternAsync(CacheKeys.CategoriesAllPattern, cancellationToken);

            return OperationResult.Success();
        }
    }
}