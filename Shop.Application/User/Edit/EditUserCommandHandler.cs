using Common.Aplication;
using Common.Application.Validation;
using Shop.Domain.UserAgg;
using Shop.Domain.UserAgg.Service;

namespace Shop.Application.User.Edit
{
    public class EditUserCommandHandler : IBaseCommandHandler<EditUserCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IDomainUserService _domainUserService;

        public EditUserCommandHandler(
            IUserRepository userRepository,
            IDomainUserService domainUserService)
        {
            _userRepository = userRepository
                ?? throw new ArgumentNullException(nameof(userRepository));
            _domainUserService = domainUserService
                ?? throw new ArgumentNullException(nameof(domainUserService));
        }

        public async Task<OperationResult> Handle(
            EditUserCommand request,
            CancellationToken cancellationToken)
        {
            var user = await _userRepository
                .GetByIdAsync(request.Id, cancellationToken);

            if (user == null)
                return OperationResult.NotFound(ValidationMessages.NotFound);

            user.Edit(
                request.Name,
                request.Family,
                request.Email,
                request.PhoneNumber,
                request.Gender,
                _domainUserService
            );

            _userRepository.Update(user);
            await _userRepository.SaveAsync(cancellationToken);

            return OperationResult.Success();
        }
    }
}