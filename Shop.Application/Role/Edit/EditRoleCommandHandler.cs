using Common.Aplication;
using Common.Application.Validation;
using Shop.Domain.RoleAgg;

namespace Shop.Application.Role.Edit
{
    public class EditRoleCommandHandler : IBaseCommandHandler<EditRoleCommand>
    {
        private readonly IRoleRepository _roleRepository;

        public EditRoleCommandHandler(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository
                ?? throw new ArgumentNullException(nameof(roleRepository));
        }

        public async Task<OperationResult> Handle(
            EditRoleCommand request,
            CancellationToken cancellationToken)
        {
            var role = await _roleRepository
                .GetByIdAsync(request.Id, cancellationToken);

            if (role == null)
                return OperationResult.NotFound(ValidationMessages.NotFound);

            role.Edit(request.Title);

            _roleRepository.Update(role);
            await _roleRepository.SaveAsync(cancellationToken);

            return OperationResult.Success();
        }
    }
}