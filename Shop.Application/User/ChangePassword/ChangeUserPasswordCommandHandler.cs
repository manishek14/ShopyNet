using Common.Aplication;
using Common.Application.Validation;
using Shop.Domain.UserAgg;
using Shop.Domain.UserAgg.Service;

namespace Shop.Application.User.ChangePassword
{
    public class ChangeUserPasswordCommandHandler : IBaseCommandHandler<ChangeUserPasswordCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;

        public ChangeUserPasswordCommandHandler(
            IUserRepository userRepository,
            IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository
                ?? throw new ArgumentNullException(nameof(userRepository));
            _passwordHasher = passwordHasher
                ?? throw new ArgumentNullException(nameof(passwordHasher));
        }

        public async Task<OperationResult> Handle(
            ChangeUserPasswordCommand request,
            CancellationToken cancellationToken)
        {
            var user = await _userRepository
                .GetByIdAsync(request.UserId, cancellationToken);

            if (user == null)
                return OperationResult.NotFound(ValidationMessages.NotFound);

            if (!_passwordHasher.Verify(request.CurrentPassword, user.Password))
                return OperationResult.Error("Current password is incorrect.");

            if (request.CurrentPassword == request.NewPassword)
                return OperationResult.Error("New password must be different from current password.");

            var hashedPassword = _passwordHasher.Hash(request.NewPassword);

            user.ChangePassword(hashedPassword);

            _userRepository.Update(user);
            await _userRepository.SaveAsync(cancellationToken);

            return OperationResult.Success();
        }
    }
}