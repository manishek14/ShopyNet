using Common.Aplication;
using Common.Application.Validation;
using Shop.Domain.UserAgg;
using Shop.Domain.UserAgg.Repository;
using Shop.Application.Caching;

namespace Shop.Application.User.ChargeWallet
{
    public class ChargeUserWalletCommandHandler : IBaseCommandHandler<ChargeUserWalletCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly ICacheService _cache;

        public ChargeUserWalletCommandHandler(IUserRepository userRepository, ICacheService cache)
        {
            _userRepository = userRepository
                ?? throw new ArgumentNullException(nameof(userRepository));
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
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

            // ✅ Invalidate user wallets cache
            await _cache.RemoveAsync(CacheKeys.UserWallets(user.Id), cancellationToken);

            return OperationResult.Success();
        }
    }
}