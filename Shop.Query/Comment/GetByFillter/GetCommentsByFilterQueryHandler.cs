using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.Comment.DTOs;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Query.Comment.GetByFilter
{
    internal class GetCommentsByFilterQueryHandler
        : IBaseQueryHandler<GetCommentsByFilterQuery, CommentFilterData>
    {
        private readonly ShopContext _shopContext;

        public GetCommentsByFilterQueryHandler(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        public async Task<CommentFilterData> Handle(
            GetCommentsByFilterQuery request,
            CancellationToken cancellationToken)
        {
            var filterParams = request.FilterParams;

            // Query 
            var query = _shopContext.Comments.AsQueryable();

            if (filterParams.UserId.HasValue && filterParams.UserId != Guid.Empty)
                query = query.Where(c => c.UserId == filterParams.UserId.Value);

            if (filterParams.ProductId.HasValue && filterParams.ProductId != Guid.Empty)
                query = query.Where(c => c.ProductId == filterParams.ProductId.Value);

            if (!string.IsNullOrWhiteSpace(filterParams.Content))
                query = query.Where(c => c.content.Contains(filterParams.Content));

            if (filterParams.Status.HasValue)
                query = query.Where(c => c.status == filterParams.Status.Value);

            if (filterParams.FromDate.HasValue)
                query = query.Where(c => c.CreatedAt >= filterParams.FromDate.Value);

            if (filterParams.ToDate.HasValue)
                query = query.Where(c => c.CreatedAt <= filterParams.ToDate.Value);

            query = query.OrderByDescending(c => c.CreatedAt);

            // Pagination
            var skip = (filterParams.PageId - 1) * filterParams.Limit;
            var comments = await query
                .Skip(skip)
                .Take(filterParams.Limit)
                .ToListAsync(cancellationToken);

            // Result
            var result = new CommentFilterData
            {
                Data = comments.MapList(),
                FilterParam = filterParams
            };

            result.GeneratePaging(query, filterParams.Limit, filterParams.PageId);

            return result;
        }
    }
}