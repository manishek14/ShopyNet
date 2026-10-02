using Common.Aplication;
using Common.Aplication.Validation;
using Common.Application.Validation;
using FluentValidation;
using Shop.Domain.UserAgg;
using Shop.Domain.UserAgg.Repository;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Application.User.AddToken
{
    // Command
    public record AddUserTokenCommand(
        Guid UserId,
        string HashJwtToken,
        string HashRefreshToken,
        DateTime TokenExpireDate,
        DateTime RefreshTokenExpireDate,
        string Device
    ) : IBaseCommand;

    // Handler
    public class AddUserTokenCommandHandler : IBaseCommandHandler<AddUserTokenCommand>
    {
        private readonly IUserRepository _userRepository;

        public AddUserTokenCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository
                ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<OperationResult> Handle(
            AddUserTokenCommand request,
            CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
            if (user == null)
                return OperationResult.NotFound("User not found.");

            var userToken = new UserToken(
                request.UserId,
                request.HashJwtToken,
                request.HashRefreshToken,
                request.TokenExpireDate,
                request.RefreshTokenExpireDate,
                request.Device
            );

            await _userRepository.AddTokenAsync(userToken, cancellationToken);
            await _userRepository.SaveAsync(cancellationToken);

            return OperationResult.Success();
        }
    }

    // Validator
    public class AddUserTokenCommandValidator : AbstractValidator<AddUserTokenCommand>
    {
        public AddUserTokenCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty()
                    .WithMessage(ValidationMessages.required("UserId"));

            RuleFor(x => x.HashJwtToken)
                .NotEmpty()
                    .WithMessage(ValidationMessages.required("HashJwtToken"));

            RuleFor(x => x.HashRefreshToken)
                .NotEmpty()
                    .WithMessage(ValidationMessages.required("HashRefreshToken"));

            RuleFor(x => x.Device)
                .NotEmpty()
                    .WithMessage(ValidationMessages.required("Device"));

            RuleFor(x => x.TokenExpireDate)
                .GreaterThan(DateTime.Now)
                    .WithMessage("TokenExpireDate must be in the future.");

            RuleFor(x => x.RefreshTokenExpireDate)
                .GreaterThan(x => x.TokenExpireDate)
                    .WithMessage("RefreshTokenExpireDate must be after TokenExpireDate.");
        }
    }
}