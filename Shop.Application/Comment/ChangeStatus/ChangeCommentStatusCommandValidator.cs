using Common.Application.Validation;
using FluentValidation;

namespace Shop.Application.Comment.ChangeStatus
{
    public class ChangeCommentStatusCommandValidator : AbstractValidator<ChangeCommentStatusCommand>
    {
        public ChangeCommentStatusCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                    .WithMessage(ValidationMessages.required("Id"));

            RuleFor(x => x.Status)
                .IsInEnum()
                    .WithMessage("Status must be a valid comment status");
        }
    }
}