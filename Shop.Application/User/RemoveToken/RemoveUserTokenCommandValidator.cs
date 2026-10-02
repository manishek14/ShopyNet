using Common.Application.Validation;
using FluentValidation;

namespace Shop.Application.User.RemoveToken
{
    public class RemoveUserTokenCommandValidator : AbstractValidator<RemoveUserTokenCommand>
    {
        public RemoveUserTokenCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty()
                    .WithMessage(ValidationMessages.required("UserId"));

            RuleFor(x => x.TokenId)
                .NotEmpty()
                    .WithMessage(ValidationMessages.required("TokenId"));
        }
    }
}