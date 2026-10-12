using Microsoft.EntityFrameworkCore;
using Shop.Application.Caching;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.Comment.DTOs;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Query.Comment.GetById
{
    internal class GetCommentByIdQueryHandler
        : IBaseQueryHandler<GetCommentByIdQuery, CommentDto>
    {
        private readonly ShopContext _shopContext;
        private readonly ICacheService _cache;

        public GetCommentByIdQueryHandler(ShopContext shopContext, ICacheService cache)
        {
            _shopContext = shopContext;
            _cache = cache;
        }

        public async Task<CommentDto> Handle(
            GetCommentByIdQuery request,
            CancellationToken cancellationToken)
        {
            var cacheKey = CacheKeys.CommentById(request.Id);

            return await _cache.GetOrSetAsync(
                cacheKey,
                async () =>
                {
                    var comment = await _shopContext.Comments
                        .AsNoTracking()
                        .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

                    return comment.Map();
                },
                TimeSpan.FromMinutes(15),
                cancellationToken);
        }
    }
}