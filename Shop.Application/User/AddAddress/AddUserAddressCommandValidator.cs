using Common.Application.Validation;
using FluentValidation;

namespace Shop.Application.User.AddAddress
{
    public class AddUserAddressCommandValidator : AbstractValidator<AddUserAddressCommand>
    {
        public AddUserAddressCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty()
                    .WithMessage(ValidationMessages.required("UserId"));

            RuleFor(x => x.Province)
                .NotEmpty()
                    .WithMessage(ValidationMessages.required("Province"))
                .MaximumLength(100)
                    .WithMessage(ValidationMessages.maxLength("Province", 100))
                .MinimumLength(2)
                    .WithMessage(ValidationMessages.minLength("Province", 2));

            RuleFor(x => x.City)
                .NotEmpty()
                    .WithMessage(ValidationMessages.required("City"))
                .MaximumLength(100)
                    .WithMessage(ValidationMessages.maxLength("City", 100))
                .MinimumLength(2)
                    .WithMessage(ValidationMessages.minLength("City", 2));

            RuleFor(x => x.PostalCode)
                .NotEmpty()
                    .WithMessage(ValidationMessages.required("PostalCode"))
                .Length(10)
                    .WithMessage("Postal code must be 10 digits")
                .Matches(@"^[0-9]{10}$")
                    .WithMessage("Postal code must contain only digits");

            RuleFor(x => x.MailingAddress)
                .NotEmpty()
                    .WithMessage(ValidationMessages.required("MailingAddress"))
                .MaximumLength(500)
                    .WithMessage(ValidationMessages.maxLength("MailingAddress", 500))
                .MinimumLength(10)
                    .WithMessage(ValidationMessages.minLength("MailingAddress", 10));

            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                    .WithMessage(ValidationMessages.required("PhoneNumber"))
                .Length(11)
                    .WithMessage("Phone number must be 11 digits")
                .Matches(@"^09[0-9]{9}$")
                    .WithMessage("Phone number must start with 09");

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

            RuleFor(x => x.NationalCode)
                .NotEmpty()
                    .WithMessage(ValidationMessages.required("NationalCode"))
                .Length(10)
                    .WithMessage("National code must be 10 digits")
                .Matches(@"^[0-9]{10}$")
                    .WithMessage("National code must contain only digits");
        }
    }
}