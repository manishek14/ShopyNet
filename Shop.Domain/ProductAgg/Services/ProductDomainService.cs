using Shop.Domain.ProductAgg;
using Shop.Domain.ProductAgg.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Infrastructure.ProductAgg.Service
{
    public class ProductDomainService : IProductDomainService
    {
        private readonly IProductRepository _productRepository;

        public ProductDomainService(IProductRepository productRepository)
        {
            _productRepository = productRepository
                ?? throw new ArgumentNullException(nameof(productRepository));
        }

        public bool IsSlugExist(string slug)
        {
            if (string.IsNullOrWhiteSpace(slug))
                return false;

            return _productRepository.Exists(p => p.Slug == slug);
        }

        public async Task<bool> IsSlugExistAsync(
            string slug,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(slug))
                return false;

            return await _productRepository.ExistsAsync(
                p => p.Slug == slug,
                cancellationToken);
        }

        public bool IsProductExist(Guid productId)
        {
            if (productId == Guid.Empty)
                return false;

            return _productRepository.Exists(p => p.Id == productId);
        }

        public async Task<bool> IsProductExistAsync(
            Guid productId,
            CancellationToken cancellationToken = default)
        {
            if (productId == Guid.Empty)
                return false;

            return await _productRepository.ExistsAsync(
                p => p.Id == productId,
                cancellationToken);
        }
    }
}