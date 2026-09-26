using Common.Application.Validation;
using FluentValidation;

namespace Shop.Application.Order.SetAddress
{
    public class SetOrderAddressCommandValidator
        : AbstractValidator<SetOrderAddressCommand>
    {
        public SetOrderAddressCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage(ValidationMessages.required("UserId"));

            RuleFor(x => x.Province)
                .NotEmpty().WithMessage(ValidationMessages.required("Province"))
                .MaximumLength(100).WithMessage(ValidationMessages.maxLength("Province", 100));

            RuleFor(x => x.City)
                .NotEmpty().WithMessage(ValidationMessages.required("City"))
                .MaximumLength(100).WithMessage(ValidationMessages.maxLength("City", 100));

            RuleFor(x => x.PostalCode)
                .NotEmpty().WithMessage(ValidationMessages.required("PostalCode"))
                .Length(10).WithMessage("Postal code must be 10 digits");

            RuleFor(x => x.MailingAddress)
                .NotEmpty().WithMessage(ValidationMessages.required("MailingAddress"))
                .MaximumLength(500).WithMessage(ValidationMessages.maxLength("MailingAddress", 500));

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage(ValidationMessages.required("PhoneNumber"))
                .Length(11).WithMessage("Phone number must be 11 digits");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(ValidationMessages.required("Name"))
                .MaximumLength(100).WithMessage(ValidationMessages.maxLength("Name", 100));

            RuleFor(x => x.Family)
                .NotEmpty().WithMessage(ValidationMessages.required("Family"))
                .MaximumLength(100).WithMessage(ValidationMessages.maxLength("Family", 100));

            RuleFor(x => x.NationalCode)
                .NotEmpty().WithMessage(ValidationMessages.required("NationalCode"))
                .Length(10).WithMessage("National code must be 10 digits");
        }
    }
}