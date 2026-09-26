using System;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Domain.RoleAgg.Services
{
    public interface IRoleDomainService
    {
        bool IsTitleExist(string title);

        Task<bool> IsTitleExistAsync(string title, CancellationToken cancellationToken = default);

        bool IsRoleExist(Guid roleId);

        Task<bool> IsRoleExistAsync(Guid roleId, CancellationToken cancellationToken = default);
    }
}