using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.Comment.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Query.Comment.GetList
{
    internal class GetCommentsListQueryHandler
        : IBaseQueryHandler<GetCommentsListQuery, List<CommentDto>>
    {
        private readonly ShopContext _shopContext;

        public GetCommentsListQueryHandler(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        public async Task<List<CommentDto>> Handle(
            GetCommentsListQuery request,
            CancellationToken cancellationToken)
        {
            var comments = await _shopContext.Comments
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync(cancellationToken);

            return comments.Map();
        }
    }
}