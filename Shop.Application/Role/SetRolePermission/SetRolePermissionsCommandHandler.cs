using Common.Aplication;
using Common.Application.Validation;
using Shop.Domain.RoleAgg;
using Shop.Application.Caching;

namespace Shop.Application.Role.SetPermissions
{
    public class SetRolePermissionsCommandHandler : IBaseCommandHandler<SetRolePermissionsCommand>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly ICacheService _cache;

        public SetRolePermissionsCommandHandler(IRoleRepository roleRepository, ICacheService cache)
        {
            _roleRepository = roleRepository
                ?? throw new ArgumentNullException(nameof(roleRepository));
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        public async Task<OperationResult> Handle(
            SetRolePermissionsCommand request,
            CancellationToken cancellationToken)
        {
            var role = await _roleRepository
                .GetByIdAsync(request.RoleId, cancellationToken);

            if (role == null)
                return OperationResult.NotFound(ValidationMessages.NotFound);

            var distinctPermissions = request.Permissions
                .Distinct()
                .ToList();

            var permissions = distinctPermissions
                .Select(p => new Shop.Domain.RoleAgg.Role.RolePermission(role.Id, p))
                .ToList();

            role.SetPermission(permissions);

            _roleRepository.Update(role);
            await _roleRepository.SaveAsync(cancellationToken);

            // Invalidate roles cache
            await _cache.RemoveByPatternAsync(CacheKeys.RolesAllPattern, cancellationToken);

            return OperationResult.Success();
        }
    }
}