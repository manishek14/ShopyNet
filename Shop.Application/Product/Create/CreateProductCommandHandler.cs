using Common.Aplication;
using Shop.Application._Utilities;
using Shop.Domain.ProductAgg;
using Shop.Domain.ProductAgg.Repository;
using Shop.Application.Caching;

namespace Shop.Application.Products.Create
{
    public class CreateProductCommandHandler : IBaseCommandHandler<CreateProductCommand>
    {
        private readonly IProductRepository _repository;
        private readonly IDirectories _localFileService;
        private readonly ICacheService _cache;

        public CreateProductCommandHandler(
            IProductRepository repository,
            IDirectories localFileService,
            ICacheService cache)
        {
            _repository = repository
                ?? throw new ArgumentNullException(nameof(repository));
            _localFileService = localFileService
                ?? throw new ArgumentNullException(nameof(localFileService));
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        public async Task<OperationResult> Handle(
            CreateProductCommand request,
            CancellationToken cancellationToken)
        {
            var imageName = await _localFileService.SaveFileAndGenerateName(
                request.ImageFile,
                Directories.ProductImages
            );

            var product = new Domain.ProductAgg.Product(
                request.Title,
                imageName,
                request.Description,
                request.CategoryId,
                request.SubCategoryId,
                request.SecondarySubCategoryId,
                request.Slug,
                request.SeoData ?? Common.Domain.ValueObject.SeoData.CreateEmpty()
            );

            await _repository.AddAsync(product, cancellationToken);

            if (request.Specifications != null && request.Specifications.Count > 0)
            {
                var specifications = new List<ProductSpecification>();

                request.Specifications.ToList().ForEach(specification =>
                {
                    specifications.Add(new ProductSpecification(
                        product.Id,
                        specification.Key,
                        specification.Value
                    ));
                });

                product.SetSpecification(specifications);
            }

            await _repository.SaveAsync(cancellationToken);

            // ✅ Cache invalidation
            await _cache.RemoveByPatternAsync(CacheKeys.ProductsByFilterPattern, cancellationToken);

            return OperationResult.Success();
        }
    }
}