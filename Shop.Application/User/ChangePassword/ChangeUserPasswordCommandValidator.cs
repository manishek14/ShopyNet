using Common.Application.Validation;
using FluentValidation;

namespace Shop.Application.User.ChangePassword
{
    public class ChangeUserPasswordCommandValidator : AbstractValidator<ChangeUserPasswordCommand>
    {
        public ChangeUserPasswordCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty()
                    .WithMessage(ValidationMessages.required("UserId"));

            RuleFor(x => x.CurrentPassword)
                .NotEmpty()
                    .WithMessage(ValidationMessages.required("CurrentPassword"))
                .MinimumLength(6)
                    .WithMessage(ValidationMessages.minLength("CurrentPassword", 6))
                .MaximumLength(100)
                    .WithMessage(ValidationMessages.maxLength("CurrentPassword", 100));

            RuleFor(x => x.NewPassword)
                .NotEmpty()
                    .WithMessage(ValidationMessages.required("NewPassword"))
                .MinimumLength(6)
                    .WithMessage(ValidationMessages.minLength("NewPassword", 6))
                .MaximumLength(100)
                    .WithMessage(ValidationMessages.maxLength("NewPassword", 100))
                .NotEqual(x => x.CurrentPassword)
                    .WithMessage("New password must be different from current password.");

            RuleFor(x => x.ConfirmNewPassword)
                .NotEmpty()
                    .WithMessage(ValidationMessages.required("ConfirmNewPassword"))
                .Equal(x => x.NewPassword)
                    .WithMessage("New password and Confirm password do not match.");
        }
    }
}