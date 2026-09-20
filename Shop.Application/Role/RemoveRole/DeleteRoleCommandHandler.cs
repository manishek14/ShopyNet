using Common.Aplication;
using Common.Application.Validation;
using Shop.Domain.RoleAgg;

namespace Shop.Application.Role.Delete
{
    public class DeleteRoleCommandHandler : IBaseCommandHandler<DeleteRoleCommand>
    {
        private readonly IRoleRepository _roleRepository;

        public DeleteRoleCommandHandler(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository
                ?? throw new ArgumentNullException(nameof(roleRepository));
        }

        public async Task<OperationResult> Handle(
            DeleteRoleCommand request,
            CancellationToken cancellationToken)
        {
            var role = await _roleRepository
                .GetByIdAsync(request.Id, cancellationToken);

            if (role == null)
                return OperationResult.NotFound(ValidationMessages.NotFound);

            _roleRepository.Remove(role);
            await _roleRepository.SaveAsync(cancellationToken);

            return OperationResult.Success();
        }
    }
}