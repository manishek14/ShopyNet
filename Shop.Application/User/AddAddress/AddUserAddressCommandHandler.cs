using Common.Aplication;
using Common.Application.Validation;
using Shop.Domain.UserAgg;

namespace Shop.Application.User.AddAddress
{
    public class AddUserAddressCommandHandler : IBaseCommandHandler<AddUserAddressCommand>
    {
        private readonly IUserRepository _userRepository;

        public AddUserAddressCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository
                ?? throw new ArgumentNullException(nameof(userRepository));
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

            return OperationResult.Success();
        }
    }
}