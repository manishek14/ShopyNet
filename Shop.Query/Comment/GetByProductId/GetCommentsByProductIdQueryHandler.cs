using Microsoft.EntityFrameworkCore;
using Shop.Application.Caching;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.Comment.DTOs;
using System;
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
        private readonly ICacheService _cache;

        public GetCommentsByProductIdQueryHandler(ShopContext shopContext, ICacheService cache)
        {
            _shopContext = shopContext;
            _cache = cache;
        }

        public async Task<List<CommentDto>> Handle(
            GetCommentsByProductIdQuery request,
            CancellationToken cancellationToken)
        {
            var cacheKey = CacheKeys.CommentsByProductId(request.ProductId);

            return await _cache.GetOrSetAsync(
                cacheKey,
                async () =>
                {
                    var comments = await _shopContext.Comments
                        .AsNoTracking()
                        .Where(c => c.ProductId == request.ProductId)
                        .OrderByDescending(c => c.CreatedAt)
                        .ToListAsync(cancellationToken);

                    return comments.MapList();
                },
                TimeSpan.FromMinutes(10),
                cancellationToken);
        }
    }
}