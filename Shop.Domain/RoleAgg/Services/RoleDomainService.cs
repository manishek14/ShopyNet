using Shop.Domain.RoleAgg;
using Shop.Domain.RoleAgg.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Infrastructure.RoleAgg.Service
{
    public class RoleDomainService : IRoleDomainService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleDomainService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository
                ?? throw new ArgumentNullException(nameof(roleRepository));
        }

        public bool IsTitleExist(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                return false;

            return _roleRepository.Exists(r => r.Title == title);
        }

        public async Task<bool> IsTitleExistAsync(
            string title,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(title))
                return false;

            return await _roleRepository.ExistsAsync(
                r => r.Title == title,
                cancellationToken);
        }

        public bool IsRoleExist(Guid roleId)
        {
            if (roleId == Guid.Empty)
                return false;

            return _roleRepository.Exists(r => r.Id == roleId);
        }

        public async Task<bool> IsRoleExistAsync(
            Guid roleId,
            CancellationToken cancellationToken = default)
        {
            if (roleId == Guid.Empty)
                return false;

            return await _roleRepository.ExistsAsync(
                r => r.Id == roleId,
                cancellationToken);
        }
    }
}