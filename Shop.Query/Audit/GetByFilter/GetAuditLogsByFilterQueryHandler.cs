using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.Audit.DTOs;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Query.Audit.GetByFilter
{
    internal class GetAuditLogsByFilterQueryHandler
        : IBaseQueryHandler<GetAuditLogsByFilterQuery, AuditLogFilterData>
    {
        private readonly ShopContext _shopContext;

        public GetAuditLogsByFilterQueryHandler(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        public async Task<AuditLogFilterData> Handle(
            GetAuditLogsByFilterQuery request,
            CancellationToken cancellationToken)
        {
            var filterParams = request.FilterParams;

            var query = _shopContext.AuditLogs.AsQueryable();

            if (filterParams.UserId.HasValue && filterParams.UserId != Guid.Empty)
                query = query.Where(a => a.UserId == filterParams.UserId.Value);

            if (!string.IsNullOrWhiteSpace(filterParams.Action))
                query = query.Where(a => a.Action == filterParams.Action);

            if (!string.IsNullOrWhiteSpace(filterParams.EntityName))
                query = query.Where(a => a.EntityName.Contains(filterParams.EntityName));

            if (!string.IsNullOrWhiteSpace(filterParams.EntityId))
                query = query.Where(a => a.EntityId == filterParams.EntityId);

            if (filterParams.FromDate.HasValue)
                query = query.Where(a => a.CreatedAt >= filterParams.FromDate.Value);

            if (filterParams.ToDate.HasValue)
                query = query.Where(a => a.CreatedAt <= filterParams.ToDate.Value);

            query = query.OrderByDescending(a => a.CreatedAt);

            var skip = (filterParams.PageId - 1) * filterParams.Limit;
            var logs = await query
                .Skip(skip)
                .Take(filterParams.Limit)
                .ToListAsync(cancellationToken);

            var result = new AuditLogFilterData
            {
                Data = logs.MapList(),
                FilterParam = filterParams
            };

            result.GeneratePaging(query, filterParams.Limit, filterParams.PageId);

            return result;
        }
    }
}