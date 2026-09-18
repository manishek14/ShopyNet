using Common.Aplication.FileUtil;
using Common.Aplication.SecurityUtil;
using Common.Aplication.Validation;
using Common.Application.Validation;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace Common.Aplication.Validation.FluentValidations
{
    public static class FluentValidations
    {
        public static IRuleBuilderOptionsConditions<T, TProperty> JustImageFile<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder, string errorMessage = "شما فقط قادر به وارد کردن عکس میباشید") where TProperty : IFormFile?
        {
            return ruleBuilder.Custom((file, context) =>
            {
                if (file == null)
                    return;

                if (!ImageValidator.IsImage(file))
                {
                    context.AddFailure(errorMessage);
                }
            });
        }

        public static IRuleBuilderOptionsConditions<T, string> ValidNationalId<T>(this IRuleBuilder<T, string> ruleBuilder, string errorMessage = "کدملی نامعتبر است")
        {
            return ruleBuilder.Custom((nationalCode, context) =>
            {
                if (IranianNationalIdChecker.IsValid(nationalCode) == false)
                    context.AddFailure(errorMessage);
            });
        }
        public static IRuleBuilderOptionsConditions<T, string> ValidPhoneNumber<T>(this IRuleBuilder<T, string> ruleBuilder, string errorMessage = ValidationMessages.InvalidPhoneNumber)
        {
            return ruleBuilder.Custom((phoneNumber, context) =>
            {
               if(string.IsNullOrWhiteSpace(phoneNumber) || phoneNumber.Length is < 11 or > 11)
                   context.AddFailure(errorMessage);

            });
        }

        public static IRuleBuilderOptionsConditions<T, TProperty> JustValidFile<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder, string errorMessage = "فایل نامعتبر است") where TProperty : IFormFile
        {
            return ruleBuilder.Custom((file, context) =>
            {
                if (file == null)
                    return;

                if (!FileValidation.IsValidFile(file))
                {
                    context.AddFailure(errorMessage);
                }
            });
        }
    }

    public static class IranianNationalIdChecker
    {
        public static bool IsValid(string? code)
        {
            if (string.IsNullOrWhiteSpace(code))
                return false;

            code = code.Trim();

            if (code.Length != 10 || !code.All(char.IsDigit))
                return false;

            // Reject trivial repeated-digit codes like 0000000000, 1111111111, ...
            if (code.Distinct().Count() == 1)
                return false;

            var digits = code.Select(c => c - '0').ToArray();
            int sum = 0;
            for (int i = 0; i < 9; i++)
                sum += digits[i] * (10 - i);

            int remainder = sum % 11;
            int control = digits[9];

            return (remainder < 2 && control == remainder) || (remainder >= 2 && control == 11 - remainder);
        }
    }
}