using Common.Application.Validation;
using FluentValidation;

namespace Shop.Application.Comment.Create
{
    public class CreateCommentCommandValidator : AbstractValidator<CreateCommentCommand>
    {
        public CreateCommentCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty()
                    .WithMessage(ValidationMessages.required("UserId"));

            RuleFor(x => x.ProductId)
                .NotEmpty()
                    .WithMessage(ValidationMessages.required("ProductId"));

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