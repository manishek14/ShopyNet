using Common.Aplication;
using Shop.Domain.UserAgg.Repository;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Application.User.ConfirmEmail
{
    public record ConfirmEmailCommand(Guid UserId, string Token) : IBaseCommand;

    public class ConfirmEmailCommandHandler : IBaseCommandHandler<ConfirmEmailCommand>
    {
        private readonly IUserRepository _userRepository;

        public ConfirmEmailCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<OperationResult> Handle(
            ConfirmEmailCommand request,
            CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
            if (user == null)
                return OperationResult.NotFound("User not found.");

            if (!user.IsEmailConfirmationTokenValid(request.Token))
                return OperationResult.Error("Invalid or expired token.");

            user.ConfirmEmail();

            _userRepository.Update(user);
            await _userRepository.SaveAsync(cancellationToken);

            return OperationResult.Success();
        }
    }
}