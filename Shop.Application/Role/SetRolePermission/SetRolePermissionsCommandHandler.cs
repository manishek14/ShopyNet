using Common.Aplication;
using Common.Application.Validation;
using Shop.Domain.RoleAgg;

namespace Shop.Application.Role.SetPermissions
{
    public class SetRolePermissionsCommandHandler : IBaseCommandHandler<SetRolePermissionsCommand>
    {
        private readonly IRoleRepository _roleRepository;

        public SetRolePermissionsCommandHandler(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository
                ?? throw new ArgumentNullException(nameof(roleRepository));
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

            return OperationResult.Success();
        }
    }
}