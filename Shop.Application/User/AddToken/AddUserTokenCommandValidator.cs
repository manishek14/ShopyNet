using Common.Application.Validation;
using FluentValidation;

namespace Shop.Application.User.AddToken
{
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