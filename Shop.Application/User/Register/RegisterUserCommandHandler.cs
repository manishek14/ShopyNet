using Common.Aplication;
using Common.Aplication.SecurityUtil; 
using Shop.Domain.UserAgg;
using Shop.Domain.UserAgg.Repository;
using Shop.Domain.UserAgg.Service;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Application.User.Register
{
    public class RegisterUserCommandHandler : IBaseCommandHandler<RegisterUserCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IDomainUserService _domainService;

        public RegisterUserCommandHandler(
            IUserRepository userRepository,
            IDomainUserService domainService)
        {
            _userRepository = userRepository;
            _domainService = domainService;
        }

        public async Task<OperationResult> Handle(
            RegisterUserCommand request,
            CancellationToken cancellationToken)
        {
            if (request.Password != request.ConfirmPassword)
                return OperationResult.Error("کلمه‌های عبور یکسان نیستند");

            var hashedPassword = Sha256Hasher.Hash(request.Password);

            var user = Domain.UserAgg.User.Register(
                request.Email,
                request.PhoneNumber,
                hashedPassword,   
                _domainService,
                request.Gender
            );

            await _userRepository.AddAsync(user, cancellationToken);
            await _userRepository.SaveAsync(cancellationToken);

            return OperationResult.Success();
        }
    }
}