using Microsoft.EntityFrameworkCore;
using Shop.Application.Caching;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.Role.DTOs;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Query.Role.GetById
{
    internal class GetRoleByIdQueryHandler
        : IBaseQueryHandler<GetRoleByIdQuery, RoleDto>
    {
        private readonly ShopContext _shopContext;
        private readonly ICacheService _cache;

        public GetRoleByIdQueryHandler(ShopContext shopContext, ICacheService cache)
        {
            _shopContext = shopContext;
            _cache = cache;
        }

        public async Task<RoleDto> Handle(
            GetRoleByIdQuery request,
            CancellationToken cancellationToken)
        {
            var cacheKey = CacheKeys.RoleById(request.Id);

            return await _cache.GetOrSetAsync(
                cacheKey,
                async () =>
                {
                    var role = await _shopContext.Roles
                        .AsNoTracking()
                        .Include(r => r.Permissions)
                        .FirstOrDefaultAsync(r => r.Id == request.Id, cancellationToken);

                    return role.Map();
                },
                TimeSpan.FromMinutes(60),
                cancellationToken);
        }
    }
}