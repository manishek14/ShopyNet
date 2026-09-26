using Common.Aplication;
using Shop.Application._Utilities;
using Shop.Domain.ProductAgg;
using Shop.Domain.ProductAgg.Repository;

namespace Shop.Application.Products.Create
{
    public class CreateProductCommandHandler : IBaseCommandHandler<CreateProductCommand>
    {
        private readonly IProductRepository _repository;
        private readonly IDirectories _localFileService;

        public CreateProductCommandHandler(
            IProductRepository repository,
            IDirectories localFileService)
        {
            _repository = repository
                ?? throw new ArgumentNullException(nameof(repository));
            _localFileService = localFileService
                ?? throw new ArgumentNullException(nameof(localFileService));
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
                request.SeoData
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

            return OperationResult.Success();
        }
    }
}