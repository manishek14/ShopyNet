using Common.Aplication;
using Common.Aplication.SecurityUtil;
using MediatR;
using Common.Application.Email;
using Shop.Application.User.GenerateEmailToken;
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
        private readonly IEmailService _emailService;
        private readonly IMediator _mediator;

        public RegisterUserCommandHandler(
            IUserRepository userRepository,
            IDomainUserService domainService,
            IEmailService emailService,
            IMediator mediator)
        {
            _userRepository = userRepository;
            _domainService = domainService;
            _emailService = emailService;
            _mediator = mediator;
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

            await _mediator.Send(new GenerateEmailConfirmationTokenCommand(user.Id), cancellationToken);

            var userWithToken = await _userRepository.GetByIdAsync(user.Id, cancellationToken);

            var confirmationLink = $"https://yourdomain.com/api/Auth/confirm-email?userId={user.Id}&token={userWithToken.EmailConfirmationToken}";

            var emailBody = $@"
                <h2>تأیید ایمیل</h2>
                <p>برای تأیید ایمیل خود روی لینک زیر کلیک کنید:</p>
                <a href='{confirmationLink}'>تأیید ایمیل</a>
                <p>این لینک تا ۲۴ ساعت اعتبار دارد.</p>
            ";

            await _emailService.SendEmailAsync(
                user.Email,
                "تأیید ایمیل",
                emailBody,
                cancellationToken);

            return OperationResult.Success();
        }
    }
}