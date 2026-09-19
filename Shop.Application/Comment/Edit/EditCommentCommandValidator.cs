using Common.Application.Validation;
using FluentValidation;

namespace Shop.Application.Comment.Edit
{
    public class EditCommentCommandValidator : AbstractValidator<EditCommentCommand>
    {
        public EditCommentCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                    .WithMessage(ValidationMessages.required("Id"));

            RuleFor(x => x.UserId)
                .NotEmpty()
                    .WithMessage(ValidationMessages.required("UserId"));

            RuleFor(x => x.Content)
                .NotEmpty()
                    .WithMessage(ValidationMessages.required("Content"))
                .MaximumLength(1000)
                    .WithMessage(ValidationMessages.maxLength("Content", 1000))
                .MinimumLength(5)
                    .WithMessage(ValidationMessages.minLength("Content", 5));
        }
    }
}