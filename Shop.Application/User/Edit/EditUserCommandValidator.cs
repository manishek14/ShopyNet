using Common.Application.Validation;
using FluentValidation;

namespace Shop.Application.User.Edit
{
    public class EditUserCommandValidator : AbstractValidator<EditUserCommand>
    {
        public EditUserCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                    .WithMessage(ValidationMessages.required("Id"));

            RuleFor(x => x.Name)
                .NotEmpty()
                    .WithMessage(ValidationMessages.required("Name"))
                .MaximumLength(100)
                    .WithMessage(ValidationMessages.maxLength("Name", 100))
                .MinimumLength(2)
                    .WithMessage(ValidationMessages.minLength("Name", 2));

            RuleFor(x => x.Family)
                .NotEmpty()
                    .WithMessage(ValidationMessages.required("Family"))
                .MaximumLength(100)
                    .WithMessage(ValidationMessages.maxLength("Family", 100))
                .MinimumLength(2)
                    .WithMessage(ValidationMessages.minLength("Family", 2));

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

            RuleFor(x => x.Gender)
                .IsInEnum()
                    .WithMessage("Gender must be a valid value");
        }
    }
}