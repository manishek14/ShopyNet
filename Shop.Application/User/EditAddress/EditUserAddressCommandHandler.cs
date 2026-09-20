using Common.Aplication;
using Common.Application.Validation;
using Shop.Domain.UserAgg;

namespace Shop.Application.User.EditAddress
{
    public class EditUserAddressCommandHandler : IBaseCommandHandler<EditUserAddressCommand>
    {
        private readonly IUserRepository _userRepository;

        public EditUserAddressCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository
                ?? throw new ArgumentNullException(nameof(userRepository));
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

            return OperationResult.Success();
        }
    }
}