using Common.Aplication;
using Shop.Domain.CommentAgg;

namespace Shop.Application.Comment.Create
{
    public class CreateCommentCommandHandler : IBaseCommandHandler<CreateCommentCommand>
    {
        private readonly ICommentRepository _commentRepository;

        public CreateCommentCommandHandler(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository
                ?? throw new ArgumentNullException(nameof(commentRepository));
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

            return OperationResult.Success();
        }
    }
}