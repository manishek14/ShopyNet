using Common.Aplication;
using Shop.Domain.UserAgg;
using Shop.Domain.UserAgg.Service;

namespace Shop.Application.User.Register
{
    public class RegisterUserCommandHandler : IBaseCommandHandler<RegisterUserCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IDomainUserService _domainUserService;

        public RegisterUserCommandHandler(
            IUserRepository userRepository,
            IDomainUserService domainUserService)
        {
            _userRepository = userRepository
                ?? throw new ArgumentNullException(nameof(userRepository));
            _domainUserService = domainUserService
                ?? throw new ArgumentNullException(nameof(domainUserService));
        }

        public async Task<OperationResult> Handle(
            RegisterUserCommand request,
            CancellationToken cancellationToken)
        {
            var user = Domain.UserAgg.User.Register(
                email: request.Email,
                phoneNumber: request.PhoneNumber,
                password: request.Password,
                domainService: _domainUserService,
                gender: request.Gender
            );

            await _userRepository.AddAsync(user, cancellationToken);
            await _userRepository.SaveAsync(cancellationToken);

            return OperationResult.Success();
        }
    }
}