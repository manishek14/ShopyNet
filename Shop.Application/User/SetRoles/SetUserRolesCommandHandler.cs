using Common.Aplication;
using Common.Application;
using Common.Application.Validation;
using Shop.Domain.UserAgg;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Application.User.SetRoles
{
    public class SetUserRolesCommandHandler : IBaseCommandHandler<SetUserRolesCommand>
    {
        private readonly Shop.Domain.UserAgg.IUserRepository _userRepository;

        public SetUserRolesCommandHandler(Shop.Domain.UserAgg.IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<OperationResult> Handle(
            SetUserRolesCommand request,
            CancellationToken cancellationToken)
        {
            var user = await _userRepository
                .GetByIdAsync(request.UserId, cancellationToken);

            if (user == null)
                return OperationResult.NotFound(ValidationMessages.NotFound);

            // Remove duplicates safely
            var distinctRoleIds = request.RoleIds?.Distinct().ToList() ?? new System.Collections.Generic.List<Guid>();

            // Create UserRole instances
            var userRoles = distinctRoleIds
                .Select(roleId => new UserRole(roleId))
                .ToList();

            // Set roles on the aggregate
            user.SetRoles(userRoles);

            _userRepository.Update(user);
            await _userRepository.SaveAsync(cancellationToken);

            return OperationResult.Success();
        }
    }
}
