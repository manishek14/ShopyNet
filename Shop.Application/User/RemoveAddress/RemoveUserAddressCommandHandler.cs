using Common.Aplication;
using Common.Application.Validation;
using Shop.Domain.UserAgg.Repository;
using Shop.Application.Caching;

namespace Shop.Application.User.RemoveAddress
{
    public class RemoveUserAddressCommandHandler : IBaseCommandHandler<RemoveUserAddressCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly ICacheService _cache;

        public RemoveUserAddressCommandHandler(IUserRepository userRepository, ICacheService cache)
        {
            _userRepository = userRepository
                ?? throw new ArgumentNullException(nameof(userRepository));
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        public async Task<OperationResult> Handle(
            RemoveUserAddressCommand request,
            CancellationToken cancellationToken)
        {
            var user = await _userRepository
                .GetByIdAsync(request.UserId, cancellationToken);

            if (user == null)
                return OperationResult.NotFound(ValidationMessages.NotFound);

            var existingAddress = user.UserAddresses
                .FirstOrDefault(a => a.Id == request.AddressId);

            if (existingAddress == null)
                return OperationResult.NotFound("Address not found!");

            user.RemoveAddress(request.AddressId);

            _userRepository.Update(user);
            await _user_repository.SaveAsync(cancellationToken);

            // ✅ Invalidate user addresses cache
            await _cache.RemoveAsync(CacheKeys.UserAddresses(user.Id), cancellationToken);

            return OperationResult.Success();
        }
    }
}