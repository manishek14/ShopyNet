using Common.Aplication;
using Common.Application.Validation;
using Shop.Domain.CommentAgg;

namespace Shop.Application.Comment.ChangeStatus
{
    public class ChangeCommentStatusCommandHandler : IBaseCommandHandler<ChangeCommentStatusCommand>
    {
        private readonly ICommentRepository _commentRepository;

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

            return OperationResult.Success();
        }
    }
}