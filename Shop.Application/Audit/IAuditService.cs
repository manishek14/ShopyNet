using System;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Application.Audit
{
    public interface IAuditService
    {
        Task LogAsync(
            Guid? userId,
            string action,
            string entityName,
            string entityId,
            string? oldValues = null,
            string? newValues = null,
            CancellationToken cancellationToken = default);
    }
}