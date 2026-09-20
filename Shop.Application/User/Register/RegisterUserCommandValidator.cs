using Common.Application.Validation;
using FluentValidation;

namespace Shop.Application.User.Register
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                    .WithMessage(ValidationMessages.required("Email"))
                .EmailAddress()
                    .WithMessage("Email format is invalid")
                .MaximumLength(200)
                    .WithMessage(ValidationMessages.maxLength("Email", 200));

            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                    .WithMessage(ValidationMessages.required("PhoneNumber"))
                .Length(11)
                    .WithMessage("Phone number must be 11 digits")
                .Matches(@"^09[0-9]{9}$")
                    .WithMessage("Phone number must start with 09");

            RuleFor(x => x.Password)
                .NotEmpty()
                    .WithMessage(ValidationMessages.required("Password"))
                .MinimumLength(6)
                    .WithMessage(ValidationMessages.minLength("Password", 6))
                .MaximumLength(100)
                    .WithMessage(ValidationMessages.maxLength("Password", 100));

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty()
                    .WithMessage(ValidationMessages.required("ConfirmPassword"))
                .Equal(x => x.Password)
                    .WithMessage("Password and ConfirmPassword do not match");
        }
    }
}