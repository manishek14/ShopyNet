using Common.Aplication;
using Common.Application.Validation;
using Shop.Application._Utilities;
using Shop.Domain.ProductAgg;

namespace Shop.Application.Products.Edit
{
    public class EditProductCommandHandler : IBaseCommandHandler<EditProductCommand>
    {
        private readonly IProductRepository _repository;
        private readonly IDirectories _localFileService;

        public EditProductCommandHandler(
            IProductRepository repository,
            IDirectories localFileService)
        {
            _repository = repository
                ?? throw new ArgumentNullException(nameof(repository));
            _localFileService = localFileService
                ?? throw new ArgumentNullException(nameof(localFileService));
        }

        public async Task<OperationResult> Handle(
            EditProductCommand request,
            CancellationToken cancellationToken)
        {
            var product = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (product == null)
                return OperationResult.NotFound(ValidationMessages.NotFound);

            var imageName = product.ImageName;

            if (request.ImageFile != null)
            {
                _localFileService.DeleteFile(
                    Directories.ProductImages,
                    product.ImageName
                );

                imageName = await _localFileService.SaveFileAndGenerateName(
                    request.ImageFile,
                    Directories.ProductImages
                );
            }

            product.Edit(
                request.Title,
                imageName,
                request.Description,
                request.CategoryId,
                request.SubCategoryId,
                request.SecondarySubCategoryId,
                request.Slug,
                request.SeoData
            );

            if (request.Specifications != null && request.Specifications.Count > 0)
            {
                var specifications = request.Specifications
                    .Select(spec => new ProductSpecification(
                        product.Id,
                        spec.Key,
                        spec.Value
                    ))
                    .ToList();

                product.SetSpecification(specifications);
            }

            _repository.Update(product);
            await _repository.SaveAsync(cancellationToken);

            return OperationResult.Success();
        }
    }
}