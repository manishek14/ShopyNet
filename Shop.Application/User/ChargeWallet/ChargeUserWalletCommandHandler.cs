using Common.Aplication;
using Common.Application.Validation;
using Shop.Domain.UserAgg;

namespace Shop.Application.User.ChargeWallet
{
    public class ChargeUserWalletCommandHandler : IBaseCommandHandler<ChargeUserWalletCommand>
    {
        private readonly IUserRepository _userRepository;

        public ChargeUserWalletCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository
                ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<OperationResult> Handle(
            ChargeUserWalletCommand request,
            CancellationToken cancellationToken)
        {
            var user = await _userRepository
                .GetByIdAsync(request.UserId, cancellationToken);

            if (user == null)
                return OperationResult.NotFound(ValidationMessages.NotFound);

            var wallet = new Wallet(
                request.UserId,
                request.Amount,
                request.Description,
                request.Type
            );

            user.ChargeWallet(wallet);

            _userRepository.Update(user);
            await _userRepository.SaveAsync(cancellationToken);

            return OperationResult.Success();
        }
    }
}