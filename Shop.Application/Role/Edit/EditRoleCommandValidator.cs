using Common.Application.Validation;
using FluentValidation;

namespace Shop.Application.Role.Edit
{
    public class EditRoleCommandValidator : AbstractValidator<EditRoleCommand>
    {
        public EditRoleCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                    .WithMessage(ValidationMessages.required("Id"));

            RuleFor(x => x.Title)
                .NotEmpty()
                    .WithMessage(ValidationMessages.required("Title"))
                .MaximumLength(100)
                    .WithMessage(ValidationMessages.maxLength("Title", 100))
                .MinimumLength(3)
                    .WithMessage(ValidationMessages.minLength("Title", 3));
        }
    }
}