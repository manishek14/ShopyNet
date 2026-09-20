using Common.Aplication;
using Common.Application.Validation;
using Shop.Application._Utilities;
using Shop.Domain.ProductAgg;

namespace Shop.Application.Products.RemoveImage
{
    public class RemoveProductImageCommandHandler : IBaseCommandHandler<RemoveProductImageCommand>
    {
        private readonly IProductRepository _repository;
        private readonly IDirectories _localFileService;

        public RemoveProductImageCommandHandler(
            IProductRepository repository,
            IDirectories localFileService)
        {
            _repository = repository
                ?? throw new ArgumentNullException(nameof(repository));
            _localFileService = localFileService
                ?? throw new ArgumentNullException(nameof(localFileService));
        }

        public async Task<OperationResult> Handle(
            RemoveProductImageCommand request,
            CancellationToken cancellationToken)
        {
            var product = await _repository.GetByIdAsync(request.ProductId, cancellationToken);

            if (product == null)
                return OperationResult.NotFound(ValidationMessages.NotFound);

            var image = product.Images.FirstOrDefault(i => i.Id == request.ImageId);

            if (image == null)
                return OperationResult.NotFound("Image not found!");

            _localFileService.DeleteFile(
                Directories.ProductGallery,
                image.ImageName
            );

            product.RemoveImage(request.ImageId);

            _repository.Update(product);
            await _repository.SaveAsync(cancellationToken);

            return OperationResult.Success();
        }
    }
}