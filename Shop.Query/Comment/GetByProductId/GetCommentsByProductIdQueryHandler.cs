using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.Comment.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Query.Comment.GetByProductId
{
    internal class GetCommentsByProductIdQueryHandler
        : IBaseQueryHandler<GetCommentsByProductIdQuery, List<CommentDto>>
    {
        private readonly ShopContext _shopContext;

        public GetCommentsByProductIdQueryHandler(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        public async Task<List<CommentDto>> Handle(
            GetCommentsByProductIdQuery request,
            CancellationToken cancellationToken)
        {
            var comments = await _shopContext.Comments
                .Where(c => c.ProductId == request.ProductId)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync(cancellationToken);

            return comments.Map();
        }
    }
}