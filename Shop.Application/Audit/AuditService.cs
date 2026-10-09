using Common.Domain;
using Microsoft.AspNetCore.Http;
using Shop.Domain.AuditAgg;
using Shop.Infrastructure.Persistent.Ef;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Application.Audit
{
    public class AuditService : IAuditService
    {
        private readonly ShopContext _shopContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuditService(ShopContext shopContext, IHttpContextAccessor httpContextAccessor)
        {
            _shopContext = shopContext;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task LogAsync(
            Guid? userId,
            string action,
            string entityName,
            string entityId,
            string? oldValues = null,
            string? newValues = null,
            CancellationToken cancellationToken = default)
        {
            var httpContext = _httpContextAccessor.HttpContext;

            var ipAddress = httpContext?.Connection.RemoteIpAddress?.ToString() ?? "unknown";
            var userAgent = httpContext?.Request.Headers["User-Agent"].ToString() ?? "unknown";

            var auditLog = new AuditLog(
                userId,
                action,
                entityName,
                entityId,
                oldValues,
                newValues,
                ipAddress,
                userAgent
            );

            await _shopContext.AuditLogs.AddAsync(auditLog, cancellationToken);
            await _shopContext.SaveChangesAsync(cancellationToken);
        }
    }
}