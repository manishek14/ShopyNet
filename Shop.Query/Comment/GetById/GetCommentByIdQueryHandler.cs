using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.Comment.DTOs;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Query.Comment.GetById
{
    internal class GetCommentByIdQueryHandler : IBaseQueryHandler<GetCommentByIdQuery, CommentDto>
    {
        private readonly ShopContext _shopContext;

        public GetCommentByIdQueryHandler(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        public async Task<CommentDto> Handle(GetCommentByIdQuery request, CancellationToken cancellationToken)
        {
            var comment = await _shopContext.Comments
                .SingleOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            return comment.Map();
        }
    }
}