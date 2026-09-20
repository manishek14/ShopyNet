using Common.Aplication;
using Common.Application.Validation;
using Shop.Domain.CommentAgg;

namespace Shop.Application.Comment.Edit
{
    public class EditCommentCommandHandler : IBaseCommandHandler<EditCommentCommand>
    {
        private readonly ICommentRepository _commentRepository;

        public EditCommentCommandHandler(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository
                ?? throw new ArgumentNullException(nameof(commentRepository));
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

            return OperationResult.Success();
        }
    }
}