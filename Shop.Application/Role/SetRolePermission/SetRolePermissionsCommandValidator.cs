using Common.Application.Validation;
using FluentValidation;

namespace Shop.Application.Role.SetPermissions
{
    public class SetRolePermissionsCommandValidator : AbstractValidator<SetRolePermissionsCommand>
    {
        public SetRolePermissionsCommandValidator()
        {
            RuleFor(x => x.RoleId)
                .NotEmpty()
                    .WithMessage(ValidationMessages.required("RoleId"));

            RuleFor(x => x.Permissions)
                .NotNull()
                    .WithMessage(ValidationMessages.required("Permissions"));
        }
    }
}