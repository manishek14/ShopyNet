using Common.Application.Validation;
using FluentValidation;

namespace Shop.Application.User.SetRoles
{
    public class SetUserRolesCommandValidator : AbstractValidator<SetUserRolesCommand>
    {
        public SetUserRolesCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty()
                    .WithMessage(ValidationMessages.required("UserId"));

            RuleFor(x => x.RoleIds)
                .NotNull()
                    .WithMessage(ValidationMessages.required("RoleIds"));

            When(x => x.RoleIds != null && x.RoleIds.Count > 0, () =>
            {
                RuleFor(x => x.RoleIds)
                    .Must(roleIds => roleIds.All(id => id != Guid.Empty))
                        .WithMessage("All RoleIds must be valid (not empty)");

                RuleFor(x => x.RoleIds)
                    .Must(roleIds => roleIds.Count <= 20)
                        .WithMessage("A user cannot have more than 20 roles");
            });
        }
    }
}