using Common.Aplication;
using Shop.Domain.UserAgg.Repository;

namespace Shop.Application.User.RemoveToken
{
    public class RemoveUserTokenCommandHandler : IBaseCommandHandler<RemoveUserTokenCommand>
    {
        private readonly IUserRepository _userRepository;

        public RemoveUserTokenCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository
                ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<OperationResult> Handle(
            RemoveUserTokenCommand request,
            CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
            if (user == null)
                return OperationResult.NotFound("User not found.");

            await _userRepository.RemoveTokenAsync(request.TokenId, cancellationToken);
            await _userRepository.SaveAsync(cancellationToken);

            return OperationResult.Success();
        }
    }
}