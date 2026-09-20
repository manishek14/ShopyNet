using Common.Application.Validation;
using FluentValidation;

namespace Shop.Application.Sellers.ChangeStatus
{
    public class ChangeSellerStatusCommandValidator : AbstractValidator<ChangeSellerStatusCommand>
    {
        public ChangeSellerStatusCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                    .WithMessage(ValidationMessages.required("Id"));

            RuleFor(x => x.Status)
                .IsInEnum()
                    .WithMessage("Status must be a valid seller status!");
        }
    }
}