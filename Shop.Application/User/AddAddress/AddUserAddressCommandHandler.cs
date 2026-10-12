using Common.Aplication;
using Common.Application.Validation;
using Shop.Domain.UserAgg;
using Shop.Domain.UserAgg.Repository;
using Shop.Application.Caching;

namespace Shop.Application.User.AddAddress
{
    public class AddUserAddressCommandHandler : IBaseCommandHandler<AddUserAddressCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly ICacheService _cache;

        public AddUserAddressCommandHandler(IUserRepository userRepository, ICacheService cache)
        {
            _user_repository = userRepository
                ?? throw new ArgumentNullException(nameof(userRepository));
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        public async Task<OperationResult> Handle(
            AddUserAddressCommand request,
            CancellationToken cancellationToken)
        {
            var user = await _userRepository
                .GetByIdAsync(request.UserId, cancellationToken);

            if (user == null)
                return OperationResult.NotFound(ValidationMessages.NotFound);

            var address = new UserAddress(
                request.Province,
                request.City,
                request.PostalCode,
                request.MailingAddress,
                request.PhoneNumber,
                request.Name,
                request.Family,
                request.NationalCode
            );

            user.AddAddress(address);

            _userRepository.Update(user);
            await _userRepository.SaveAsync(cancellationToken);

            // ✅ Invalidate user addresses cache
            await _cache.RemoveAsync(CacheKeys.UserAddresses(user.Id), cancellationToken);

            return OperationResult.Success();
        }
    }
}