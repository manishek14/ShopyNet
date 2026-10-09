using Common.Query.Filter;
using System;
using static Common.Query.Filter.BaseFilter;

namespace Shop.Query.Audit.DTOs
{
    public class AuditLogFilterParams : BaseFilterParam
    {
        public Guid? UserId { get; set; }
        public string? Action { get; set; }
        public string? EntityName { get; set; }
        public string? EntityId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}