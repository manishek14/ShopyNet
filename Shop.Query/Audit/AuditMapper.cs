using Shop.Query.Audit.DTOs;
using System.Collections.Generic;

namespace Shop.Query.Audit
{
    internal static class AuditMapper
    {
        public static AuditLogDto? Map(this Domain.AuditAgg.AuditLog? auditLog)
        {
            if (auditLog is null)
                return null;

            return new AuditLogDto
            {
                Id = auditLog.Id,
                UserId = auditLog.UserId,
                Action = auditLog.Action,
                EntityName = auditLog.EntityName,
                EntityId = auditLog.EntityId,
                OldValues = auditLog.OldValues,
                NewValues = auditLog.NewValues,
                IpAddress = auditLog.IpAddress,
                UserAgent = auditLog.UserAgent,
                CreatedDate = auditLog.CreatedAt
            };
        }

        public static List<AuditLogDto> MapList(this List<Domain.AuditAgg.AuditLog>? auditLogs)
        {
            var result = new List<AuditLogDto>();
            if (auditLogs == null)
                return result;

            foreach (var log in auditLogs)
                result.Add(Map(log)!);

            return result;
        }
    }
}