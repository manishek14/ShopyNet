using Common.Application.Validation;
using FluentValidation;

namespace Shop.Application.User.RemoveAddress
{
    public class RemoveUserAddressCommandValidator : AbstractValidator<RemoveUserAddressCommand>
    {
        public RemoveUserAddressCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty()
                    .WithMessage(ValidationMessages.required("UserId"));

            RuleFor(x => x.AddressId)
                .NotEmpty()
                    .WithMessage(ValidationMessages.required("AddressId"));
        }
    }
}