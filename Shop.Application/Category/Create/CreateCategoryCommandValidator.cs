using Common.Application.Validation;
using FluentValidation;

namespace Shop.Application.Category.Create
{
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                    .WithMessage(ValidationMessages.required("Title"))
                .MaximumLength(200)
                    .WithMessage(ValidationMessages.maxLength("Title", 200))
                .MinimumLength(3)
                    .WithMessage(ValidationMessages.minLength("Title", 3));

            RuleFor(x => x.Slug)
                .NotEmpty()
                    .WithMessage(ValidationMessages.required("Slug"))
                .MaximumLength(200)
                    .WithMessage(ValidationMessages.maxLength("Slug", 200))
                .MinimumLength(3)
                    .WithMessage(ValidationMessages.minLength("Slug", 3))
                .Matches(@"^[a-z0-9]+(?:-[a-z0-9]+)*$")
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