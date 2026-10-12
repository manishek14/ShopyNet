using Common.Aplication;
using Common.Application.Validation;
using Shop.Domain.CommentAgg;
using Shop.Application.Caching;

namespace Shop.Application.Comment.ChangeStatus
{
    public class ChangeCommentStatusCommandHandler : IBaseCommandHandler<ChangeCommentStatusCommand>
    {
        private readonly ICommentRepository _commentRepository;
        private readonly ICacheService _cache;

        public ChangeCommentStatusCommandHandler(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository
                ?? throw new ArgumentNullException(nameof(commentRepository));
        }

        public async Task<OperationResult> Handle(
            ChangeCommentStatusCommand request,
            CancellationToken cancellationToken)
        {
            var comment = await _commentRepository
                .GetByIdAsync(request.Id, cancellationToken);

            if (comment == null)
                return OperationResult.NotFound(ValidationMessages.NotFound);

            comment.ChangeStatus(request.Status);

            _commentRepository.Update(comment);
            await _commentRepository.SaveAsync(cancellationToken);

            // ✅ Invalidate comment caches
            await _cache.RemoveAsync(CacheKeys.CommentById(comment.Id), cancellationToken);
            await _cache.RemoveAsync(CacheKeys.CommentsByProductId(comment.ProductId), cancellationToken);

            return OperationResult.Success();
        }
    }
}