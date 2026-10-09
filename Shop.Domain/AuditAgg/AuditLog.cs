using Common.Domain;
using System;

namespace Shop.Domain.AuditAgg
{
    public class AuditLog : BaseEntity
    {
        private AuditLog() { }

        public AuditLog(
            Guid? userId,
            string action,
            string entityName,
            string entityId,
            string? oldValues,
            string? newValues,
            string? ipAddress,
            string? userAgent)
        {
            UserId = userId;
            Action = action;
            EntityName = entityName;
            EntityId = entityId;
            OldValues = oldValues;
            NewValues = newValues;
            IpAddress = ipAddress;
            UserAgent = userAgent;
            CreatedAt = DateTime.Now;
        }

        public Guid? UserId { get; private set; }
        public string Action { get; private set; } = string.Empty;
        public string EntityName { get; private set; } = string.Empty;
        public string EntityId { get; private set; } = string.Empty;
        public string? OldValues { get; private set; }
        public string? NewValues { get; private set; }
        public string? IpAddress { get; private set; }
        public string? UserAgent { get; private set; }
        public DateTime CreatedAt { get; private set; }
    }
}