using Shop.Domain.ProductAgg;
using Shop.Domain.SellerAgg;
using Shop.Domain.SellerAgg.Services;
using Shop.Domain.UserAgg.Repository;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Infrastructure.SellerAgg.Service
{
    public class SellerDomainService : ISellerDomainService
    {
        private readonly ISellerRepository _sellerRepository;
        private readonly IUserRepository _userRepository;
        private readonly IProductRepository _productRepository;

        public SellerDomainService(
            ISellerRepository sellerRepository,
            IUserRepository userRepository,
            IProductRepository productRepository)
        {
            _sellerRepository = sellerRepository
                ?? throw new ArgumentNullException(nameof(sellerRepository));
            _userRepository = userRepository
                ?? throw new ArgumentNullException(nameof(userRepository));
            _productRepository = productRepository
                ?? throw new ArgumentNullException(nameof(productRepository));
        }

        public bool IsNationalCodeExist(string nationalCode)
        {
            if (string.IsNullOrWhiteSpace(nationalCode))
                return false;

            return _sellerRepository.Exists(s => s.NationalCode == nationalCode);
        }

        public async Task<bool> IsNationalCodeExistAsync(
            string nationalCode,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(nationalCode))
                return false;

            return await _sellerRepository.ExistsAsync(
                s => s.NationalCode == nationalCode,
                cancellationToken);
        }

        public bool IsShopNameExist(string shopName)
        {
            if (string.IsNullOrWhiteSpace(shopName))
                return false;

            return _sellerRepository.Exists(s => s.ShopName == shopName);
        }

        public async Task<bool> IsShopNameExistAsync(
            string shopName,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(shopName))
                return false;

            return await _sellerRepository.ExistsAsync(
                s => s.ShopName == shopName,
                cancellationToken);
        }

        public bool IsUserExist(Guid userId)
        {
            if (userId == Guid.Empty)
                return false;

            return _userRepository.Exists(u => u.Id == userId);
        }

        public Task<bool> IsUserExistAsync(
            Guid userId,
            CancellationToken cancellationToken = default)
        {
            if (userId == Guid.Empty)
                return Task.FromResult(false);

            return Task.FromResult(_userRepository.Exists(u => u.Id == userId));
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