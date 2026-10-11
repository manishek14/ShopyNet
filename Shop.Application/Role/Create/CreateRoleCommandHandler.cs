using Common.Aplication;
using Shop.Domain.RoleAgg;
using Shop.Application.Caching;

namespace Shop.Application.Role.Create
{
    public class CreateRoleCommandHandler : IBaseCommandHandler<CreateRoleCommand>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly ICacheService _cache;

        public CreateRoleCommandHandler(IRoleRepository roleRepository, ICacheService cache)
        {
            _roleRepository = roleRepository
                ?? throw new ArgumentNullException(nameof(roleRepository));
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        public async Task<OperationResult> Handle(
            CreateRoleCommand request,
            CancellationToken cancellationToken)
        {
            var permissions = request.Permissions
                .Distinct()
                .Select(p => new Shop.Domain.RoleAgg.Role.RolePermission(Guid.NewGuid(), p))
                .ToList();

            var role = new Domain.RoleAgg.Role(request.Title, permissions);

            await _roleRepository.AddAsync(role, cancellationToken);
            await _roleRepository.SaveAsync(cancellationToken);

            return OperationResult.Success();
        }
    }
}