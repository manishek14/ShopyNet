using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using SixLabors.ImageSharp;
using System.ComponentModel.DataAnnotations;
using System.IO;
using static System.Net.Mime.MediaTypeNames;

namespace Common.Application.Validation.CustomValidation.IFormFile
{
    public class FileImageAttribute : ValidationAttribute, IClientModelValidator
    {
        public override bool IsValid(object? value)
        {
            var fileInput = value as Microsoft.AspNetCore.Http.IFormFile;
            if (fileInput == null)
                return true;

            return fileInput.IsImage();
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            if (!context.Attributes.ContainsKey("data-val"))
                context.Attributes.Add("data-val", "true");
            context.Attributes.Add("accept", "image/*");
            context.Attributes.Add("data-val-fileImage", ErrorMessage);
        }
    }

    public static class Validation
    {
        public static bool IsImage(this Microsoft.AspNetCore.Http.IFormFile file)
        {
            if (file == null || file.Length == 0)
                return false;

            try
            {
                using var stream = file.OpenReadStream();
                using var image = SixLabors.ImageSharp.Image.Load(stream);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}