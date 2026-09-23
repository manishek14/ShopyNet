using Common.Application.Validation;
using FluentValidation;

namespace Shop.Application.Category.Edit
{
    public class EditCategoryCommandValidator : AbstractValidator<EditCategoryCommand>
    {
        public EditCategoryCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                    .WithMessage(ValidationMessages.required("Id"));

            RuleFor(x => x.Title)
                .NotEmpty()
                    .WithMessage(ValidationMessages.required("Title"))
                .MaximumLength(200)
                    .WithMessage(ValidationMessages.maxLength("Title", 200))
                .MinimumLength(3)
                    .WithMessage(ValidationMessages.minLength("Title", 3));

            RuleFor(x => x.Slug)
                .MaximumLength(200)
                    .WithMessage(ValidationMessages.maxLength("Slug", 200))
                .Matches(@"^[a-z0-9]+(?:-[a-z0-9]+)*$")
                    .When(x => !string.IsNullOrWhiteSpace(x.Slug))
                    .WithMessage("Slug must contain only lowercase letters, numbers, and hyphens");

            RuleFor(x => x.SeoData)
                .NotNull()
                    .WithMessage(ValidationMessages.required("SeoData"));

            When(x => x.SeoData != null, () =>
            {
                RuleFor(x => x.SeoData.MetaData)
                    .NotEmpty()
                        .WithMessage(ValidationMessages.required("MetaTitle"))
                    .MaximumLength(60)
                        .WithMessage(ValidationMessages.maxLength("MetaTitle", 60));

                RuleFor(x => x.SeoData.MetaDescription)
                    .NotEmpty()
                        .WithMessage(ValidationMessages.required("MetaDescription"))
                    .MaximumLength(160)
                        .WithMessage(ValidationMessages.maxLength("MetaDescription", 160));

                RuleFor(x => x.SeoData.CanonicalUrl)
                    .MaximumLength(500)
                        .WithMessage(ValidationMessages.maxLength("Canonical", 500));

                RuleFor(x => x.SeoData.MetaKeywords)
                    .MaximumLength(500)
                        .WithMessage(ValidationMessages.maxLength("Keywords", 500));
            });
        }
    }
}