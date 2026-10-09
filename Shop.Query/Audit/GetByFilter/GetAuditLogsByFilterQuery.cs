using Shop.Query.Audit.DTOs;

namespace Shop.Query.Audit.GetByFilter
{
    public record GetAuditLogsByFilterQuery(AuditLogFilterParams FilterParams)
        : IBaseQuery<AuditLogFilterData>;
}