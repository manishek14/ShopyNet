using Microsoft.EntityFrameworkCore;
using Shop.Application.Caching;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.User.DTOs;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Query.User.GetById
{
    internal class GetUserByIdQueryHandler
        : IBaseQueryHandler<GetUserByIdQuery, UserDto>
    {
        private readonly ShopContext _shopContext;
        private readonly ICacheService _cache;

        public GetUserByIdQueryHandler(ShopContext shopContext, ICacheService cache)
        {
            _shopContext = shopContext;
            _cache = cache;
        }

        public async Task<UserDto> Handle(
            GetUserByIdQuery request,
            CancellationToken cancellationToken)
        {
            var cacheKey = CacheKeys.UserById(request.Id);

            return await _cache.GetOrSetAsync(
                cacheKey,
                async () =>
                {
                    var user = await _shopContext.Users
                        .AsNoTracking()
                        .Include(u => u.UserRoles)
                        .Include(u => u.UserAddresses)
                        .FirstOrDefaultAsync(u => u.Id == request.Id, cancellationToken);

                    return user.Map();
                },
                TimeSpan.FromMinutes(15),
                cancellationToken);
        }
    }
}