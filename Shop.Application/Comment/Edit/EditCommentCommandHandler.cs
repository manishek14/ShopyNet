using Common.Aplication;
using Common.Application.Validation;
using Shop.Domain.CommentAgg;
using Shop.Application.Caching;

namespace Shop.Application.Comment.Edit
{
    public class EditCommentCommandHandler : IBaseCommandHandler<EditCommentCommand>
    {
        private readonly ICommentRepository _commentRepository;
        private readonly ICacheService _cache;

        public EditCommentCommandHandler(ICommentRepository commentRepository, ICacheService cache)
        {
            _commentRepository = commentRepository
                ?? throw new ArgumentNullException(nameof(commentRepository));
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        public async Task<OperationResult> Handle(
            EditCommentCommand request,
            CancellationToken cancellationToken)
        {
            var comment = await _commentRepository
                .GetByIdAsync(request.Id, cancellationToken);

            if (comment == null)
                return OperationResult.NotFound(ValidationMessages.NotFound);

            if (comment.UserId != request.UserId)
                return OperationResult.Error("You are not authorized to edit this comment.");

            comment.Edit(request.Content);

            _commentRepository.Update(comment);
            await _commentRepository.SaveAsync(cancellationToken);

            // ✅ Invalidate comment caches
            await _cache.RemoveAsync(CacheKeys.CommentById(comment.Id), cancellationToken);
            await _cache.RemoveAsync(CacheKeys.CommentsByProductId(comment.ProductId), cancellationToken);

            return OperationResult.Success();
        }
    }
}