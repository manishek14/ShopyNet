using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.Role.DTOs;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Query.Role.GetByFilter
{
    internal class GetRolesByFilterQueryHandler
        : IBaseQueryHandler<GetRolesByFilterQuery, RoleFilterData>
    {
        private readonly ShopContext _shopContext;

        public GetRolesByFilterQueryHandler(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        public async Task<RoleFilterData> Handle(
            GetRolesByFilterQuery request,
            CancellationToken cancellationToken)
        {
            var filterParams = request.FilterParams;

            var query = _shopContext.Roles
                .Include(r => r.Permissions)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(filterParams.Title))
            {
                query = query.Where(r => r.Title.Contains(filterParams.Title));
            }

            query = query.OrderByDescending(r => r.CreatedAt);

            var skip = (filterParams.PageId - 1) * filterParams.Limit;
            var roles = await query
                .Skip(skip)
                .Take(filterParams.Limit)
                .ToListAsync(cancellationToken);

            var result = new RoleFilterData
            {
                Data = roles.MapList(),
                FilterParam = filterParams
            };

            result.GeneratePaging(query, filterParams.Limit, filterParams.PageId);

            return result;
        }
    }
}