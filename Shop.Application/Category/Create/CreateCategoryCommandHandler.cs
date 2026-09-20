using Common.Aplication;
using Shop.Domain.CategoryAgg;

namespace Shop.Application.Category.Create
{
    public class CreateCategoryCommandHandler : IBaseCommandHandler<CreateCategoryCommand>
    {
        private readonly ICategoryRepository _categoryRepository;

        public CreateCategoryCommandHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
        }

        public async Task<OperationResult> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = new Shop.Domain.CategoryAgg.Category(
                request.Title,
                request.Slug,
                request.SeoData
            );

            await _categoryRepository.AddAsync(category);
            return OperationResult.Success();
        }
    }
}