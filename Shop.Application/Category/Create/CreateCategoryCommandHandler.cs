using Common.Aplication;
using Shop.Domain.CategoryAgg;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Application.Category.Create
{
    public class CreateCategoryCommandHandler : IBaseCommandHandler<CreateCategoryCommand>
    {
        private readonly ICategoryRepository _categoryRepository;

        public CreateCategoryCommandHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<OperationResult> Handle(
            CreateCategoryCommand request,
            CancellationToken cancellationToken)
        {
            var existingCategory = await _categoryRepository.GetBySlugAsync(request.Slug, cancellationToken);
            if (existingCategory != null)
                return OperationResult.Error($"Category with slug '{request.Slug}' already exists.");

            var category = new Domain.CategoryAgg.Category(
                request.Title,
                request.Slug,
                request.SeoData
            );

            await _categoryRepository.AddAsync(category, cancellationToken);
            await _categoryRepository.SaveAsync(cancellationToken);

            return OperationResult.Success();
        }
    }
}