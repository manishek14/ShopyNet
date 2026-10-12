using Common.Aplication;
using Shop.Domain.CommentAgg;
using Shop.Application.Caching;

namespace Shop.Application.Comment.Create
{
    public class CreateCommentCommandHandler : IBaseCommandHandler<CreateCommentCommand>
    {
        private readonly ICommentRepository _commentRepository;
        private readonly ICacheService _cache;

        public CreateCommentCommandHandler(ICommentRepository commentRepository, ICacheService cache)
        {
            _commentRepository = commentRepository
                ?? throw new ArgumentNullException(nameof(commentRepository));
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        public async Task<OperationResult> Handle(
            CreateCommentCommand request,
            CancellationToken cancellationToken)
        {
            var comment = new Shop.Domain.CommentAgg.Comment(
                request.UserId,
                request.ProductId,
                request.Content
            );

            await _commentRepository.AddAsync(comment, cancellationToken);
            await _commentRepository.SaveAsync(cancellationToken);

            // ✅ Invalidate comments cache for product
            await _cache.RemoveAsync(CacheKeys.CommentsByProductId(request.ProductId), cancellationToken);

            return OperationResult.Success();
        }
    }
}