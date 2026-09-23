using Common.Aplication;
using Common.Application.Validation;
using Shop.Domain.UserAgg;

namespace Shop.Application.User.RemoveAddress
{
    public class RemoveUserAddressCommandHandler : IBaseCommandHandler<RemoveUserAddressCommand>
    {
        private readonly IUserRepository _userRepository;

        public RemoveUserAddressCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository
                ?? throw new ArgumentNullException(nameof(userRepository));
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
            await _userRepository.SaveAsync(cancellationToken);

            return OperationResult.Success();
        }
    }
}