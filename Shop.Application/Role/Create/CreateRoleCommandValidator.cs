using Common.Application.Validation;
using FluentValidation;

namespace Shop.Application.Role.Create
{
    public class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
    {
        public CreateRoleCommandValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                    .WithMessage(ValidationMessages.required("Title"))
                .MaximumLength(100)
                    .WithMessage(ValidationMessages.maxLength("Title", 100))
                .MinimumLength(3)
                    .WithMessage(ValidationMessages.minLength("Title", 3));

            RuleFor(x => x.Permissions)
                .NotNull()
                    .WithMessage(ValidationMessages.required("Permissions"))
                .Must(p => p != null && p.Count > 0)
                    .WithMessage("At least one permission is required")
                .Must(p => p == null || p.Count <= 32)
                    .WithMessage("Too many permissions");
        }
    }
}