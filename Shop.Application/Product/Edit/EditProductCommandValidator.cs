using Common.Application.Validation;
using FluentValidation;

namespace Shop.Application.Products.Edit
{
    public class EditProductCommandValidator : AbstractValidator<EditProductCommand>
    {
        public EditProductCommandValidator()
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

            RuleFor(x => x.Description)
                .NotEmpty()
                    .WithMessage(ValidationMessages.required("Description"))
                .MaximumLength(5000)
                    .WithMessage(ValidationMessages.maxLength("Description", 5000))
                .MinimumLength(10)
                    .WithMessage(ValidationMessages.minLength("Description", 10));

            When(x => x.ImageFile != null, () =>
            {
                RuleFor(x => x.ImageFile)
                    .Must(file => file!.Length > 0)
                        .WithMessage("Image file cannot be empty")
                    .Must(file => file!.Length <= 5 * 1024 * 1024)
                        .WithMessage("Image size cannot exceed 5MB");
            });

            RuleFor(x => x.CategoryId)
                .NotEmpty()
                    .WithMessage(ValidationMessages.required("CategoryId"));

            RuleFor(x => x.SubCategoryId)
                .NotEmpty()
                    .WithMessage(ValidationMessages.required("SubCategoryId"));

            RuleFor(x => x.SecondarySubCategoryId)
                .NotEmpty()
                    .WithMessage(ValidationMessages.required("SecondarySubCategoryId"));

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
            });
        }
    }
}