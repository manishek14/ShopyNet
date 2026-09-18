using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using Image = SixLabors.ImageSharp.Image;

namespace Common.Aplication.SecurityUtil
{
   public static class ImageValidator
    {
        public static bool IsImage(this IFormFile? file)
        {
            if (file == null) return false;
            try
            {
                using var stream = file.OpenReadStream();
                var info = Image.Identify(stream);
                return info != null;
            }
            catch
            {
                return false;
            }
        }
    }
}
