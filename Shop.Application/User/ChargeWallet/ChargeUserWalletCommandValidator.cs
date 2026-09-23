using Common.Application.Validation;
using FluentValidation;

namespace Shop.Application.User.ChargeWallet
{
    public class ChargeUserWalletCommandValidator : AbstractValidator<ChargeUserWalletCommand>
    {
        public ChargeUserWalletCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty()
                    .WithMessage(ValidationMessages.required("UserId"));

            RuleFor(x => x.Amount)
                .NotEmpty()
                    .WithMessage(ValidationMessages.required("Amount"))
                .GreaterThan(0)
                    .WithMessage("Amount must be greater than zero")
                .LessThanOrEqualTo(100_000_000)
                    .WithMessage("Amount cannot exceed 100,000,000");

            RuleFor(x => x.Description)
                .NotEmpty()
                    .WithMessage(ValidationMessages.required("Description"))
                .MaximumLength(500)
                    .WithMessage(ValidationMessages.maxLength("Description", 500))
                .MinimumLength(3)
                    .WithMessage(ValidationMessages.minLength("Description", 3));

            RuleFor(x => x.Type)
                .IsInEnum()
                    .WithMessage("Wallet type must be a valid value");
        }
    }
}