using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.Comment.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Query.Comment.GetWithRepliesByProductId
{
    internal class GetCommentsWithRepliesByProductIdQueryHandler
        : IBaseQueryHandler<GetCommentsWithRepliesByProductIdQuery, List<CommentWithReplyDto>>
    {
        private readonly ShopContext _shopContext;

        public GetCommentsWithRepliesByProductIdQueryHandler(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        public async Task<List<CommentWithReplyDto>> Handle(
            GetCommentsWithRepliesByProductIdQuery request,
            CancellationToken cancellationToken)
        {
            var comments = await _shopContext.Comments
                .Where(c => c.ProductId == request.ProductId)
                .ToListAsync(cancellationToken);

            return comments.MapWithReplies();
        }
    }
}