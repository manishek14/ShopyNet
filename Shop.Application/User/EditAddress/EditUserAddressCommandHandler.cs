using Common.Aplication;
using Common.Application.Validation;
using Shop.Domain.UserAgg.Repository;
using Shop.Application.Caching;

namespace Shop.Application.User.EditAddress
{
    public class EditUserAddressCommandHandler : IBaseCommandHandler<EditUserAddressCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly ICacheService _cache;

        public EditUserAddressCommandHandler(IUserRepository userRepository, ICacheService cache)
        {
            _user_repository = userRepository
                ?? throw new ArgumentNullException(nameof(userRepository));
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        public async Task<OperationResult> Handle(
            EditUserAddressCommand request,
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

            existingAddress.Edit(
                request.Province,
                request.City,
                request.PostalCode,
                request.MailingAddress,
                request.PhoneNumber,
                request.Name,
                request.Family,
                request.NationalCode
            );

            _userRepository.Update(user);
            await _userRepository.SaveAsync(cancellationToken);

            // ✅ Invalidate user addresses cache
            await _cache.RemoveAsync(CacheKeys.UserAddresses(user.Id), cancellationToken);

            return OperationResult.Success();
        }
    }
}