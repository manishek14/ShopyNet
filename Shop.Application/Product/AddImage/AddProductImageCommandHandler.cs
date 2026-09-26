using Common.Aplication;
using Common.Application.Validation;
using Shop.Application._Utilities;
using Shop.Domain.ProductAgg;
using Shop.Domain.ProductAgg.Repository;

namespace Shop.Application.Products.AddImage
{
    public class AddProductImageCommandHandler : IBaseCommandHandler<AddProductImageCommand>
    {
        private readonly IProductRepository _repository;
        private readonly IDirectories _localFileService;

        public AddProductImageCommandHandler(
            IProductRepository repository,
            IDirectories localFileService)
        {
            _repository = repository
                ?? throw new ArgumentNullException(nameof(repository));
            _localFileService = localFileService
                ?? throw new ArgumentNullException(nameof(localFileService));
        }

        public async Task<OperationResult> Handle(
            AddProductImageCommand request,
            CancellationToken cancellationToken)
        {
            var product = await _repository.GetByIdAsync(request.ProductId, cancellationToken);

            if (product == null)
                return OperationResult.NotFound(ValidationMessages.NotFound);

            var imageName = await _localFileService.SaveFileAndGenerateName(
                request.ImageFile,
                Directories.ProductGallery
            );

            var productImage = new ProductImage(
                product.Id,
                imageName,
                request.Sequence
            );

            product.AddImage(productImage);

            _repository.Update(product);
            await _repository.SaveAsync(cancellationToken);

            return OperationResult.Success();
        }
    }
}