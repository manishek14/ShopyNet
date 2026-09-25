using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.User.DTOs;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Query.User.GetByFilter
{
    internal class GetUsersByFilterQueryHandler
        : IBaseQueryHandler<GetUsersByFilterQuery, UserFilterData>
    {
        private readonly ShopContext _shopContext;

        public GetUsersByFilterQueryHandler(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        public async Task<UserFilterData> Handle(
            GetUsersByFilterQuery request,
            CancellationToken cancellationToken)
        {
            var filterParams = request.FilterParams;

            var query = _shopContext.Users
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(filterParams.Name))
            {
                query = query.Where(u => u.Name.Contains(filterParams.Name));
            }

            if (!string.IsNullOrWhiteSpace(filterParams.Family))
            {
                query = query.Where(u => u.Family.Contains(filterParams.Family));
            }

            if (!string.IsNullOrWhiteSpace(filterParams.Email))
            {
                query = query.Where(u => u.Email.Contains(filterParams.Email));
            }

            if (!string.IsNullOrWhiteSpace(filterParams.PhoneNumber))
            {
                query = query.Where(u => u.PhoneNumber == filterParams.PhoneNumber);
            }

            if (filterParams.Gender.HasValue)
            {
                query = query.Where(u => u.Gender == filterParams.Gender.Value);
            }

            if (filterParams.IsActive.HasValue)
            {
                query = query.Where(u => u.IsActive == filterParams.IsActive.Value);
            }

            query = query.OrderByDescending(u => u.CreatedAt);

            var skip = (filterParams.PageId - 1) * filterParams.Limit;
            var users = await query
                .Skip(skip)
                .Take(filterParams.Limit)
                .ToListAsync(cancellationToken);

            var result = new UserFilterData
            {
                Data = users.MapList(),
                FilterParam = filterParams
            };

            result.GeneratePaging(query, filterParams.Limit, filterParams.PageId);

            return result;
        }
    }
}