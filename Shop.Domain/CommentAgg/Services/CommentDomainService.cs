using Shop.Domain.CommentAgg;
using Shop.Domain.ProductAgg.Repository;
using Shop.Domain.CommentAgg.Services;
using Shop.Domain.ProductAgg;
using Shop.Domain.ProductAgg.Repository;
using Shop.Domain.UserAgg.Repository;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Infrastructure.CommentAgg.Service
{
    public class CommentDomainService : ICommentDomainService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUserRepository _userRepository;

        public CommentDomainService(
            ICommentRepository commentRepository,
            IProductRepository productRepository,
            IUserRepository userRepository)
        {
            _commentRepository = commentRepository
                ?? throw new ArgumentNullException(nameof(commentRepository));
            _productRepository = productRepository
                ?? throw new ArgumentNullException(nameof(productRepository));
            _userRepository = userRepository
                ?? throw new ArgumentNullException(nameof(userRepository));
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

        public bool IsUserExist(Guid userId)
        {
            if (userId == Guid.Empty)
                return false;

            return _userRepository.Exists(u => u.Id == userId);
        }

        public Task<bool> IsUserExistAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            if (userId == Guid.Empty)
                return Task.FromResult(false);
            return Task.FromResult(_userRepository.Exists(u => u.Id == userId));
        }

        public bool IsCommentLimitExceeded(Guid userId)
        {
            if (userId == Guid.Empty)
                return false;

            const int maxComments = 100;
            var comments = _commentRepository
                .FindAsync(c => c.UserId == userId)
                .Result;

            return comments.Count >= maxComments;
        }
    }
}