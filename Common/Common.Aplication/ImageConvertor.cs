using SkiaSharp;
using System;
using System.IO;

namespace Common.Application
{
    public static class ImageConvertor
    {
        public static void CreateBitMap(
            string inputImagePath,
            string outputPath,
            int newWidth,
            int newHeight)
        {
            if (string.IsNullOrWhiteSpace(inputImagePath))
                throw new ArgumentException("Input image path is required.", nameof(inputImagePath));

            if (string.IsNullOrWhiteSpace(outputPath))
                throw new ArgumentException("Output path is required.", nameof(outputPath));

            if (newWidth <= 0)
                throw new ArgumentException("Width must be greater than zero.", nameof(newWidth));

            if (newHeight <= 0)
                throw new ArgumentException("Height must be greater than zero.", nameof(newHeight));

            var inputDirectory = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                inputImagePath.TrimStart('/', '\\').Replace("/", Path.DirectorySeparatorChar.ToString())
            );

            if (!File.Exists(inputDirectory))
                throw new FileNotFoundException($"Input image not found: {inputDirectory}");

            var pathSplit = inputImagePath.Split('/');
            var imageName = pathSplit[^1];

            var folderName = Path.Combine(
                Directory.GetCurrentDirectory(),
                outputPath.TrimStart('/', '\\').Replace("/", Path.DirectorySeparatorChar.ToString())
            );

            if (!Directory.Exists(folderName))
                Directory.CreateDirectory(folderName);

            var outputDirectory = Path.Combine(folderName, imageName);

            ResizeImage(inputDirectory, outputDirectory, newWidth, newHeight);
        }

        private static void ResizeImage(
            string inputPath,
            string outputPath,
            int newWidth,
            int newHeight)
        {
            using var inputStream = File.OpenRead(inputPath);
            using var originalBitmap = SKBitmap.Decode(inputStream);

            if (originalBitmap == null)
                throw new InvalidDataException($"Failed to decode image: {inputPath}");

            using var resizedBitmap = originalBitmap.Resize(
                new SKImageInfo(newWidth, newHeight),
                new SKSamplingOptions(SKFilterMode.Linear, SKMipmapMode.Linear)
            );

            if (resizedBitmap == null)
                throw new InvalidOperationException("Failed to resize image.");

            using var image = SKImage.FromBitmap(resizedBitmap);
            using var data = image.Encode(SKEncodedImageFormat.Jpeg, 50);
            using var outputStream = File.OpenWrite(outputPath);

            data.SaveTo(outputStream);
        }

        public static void CompressImage(
            string imagePath,
            string destPath,
            long quality)
        {
            if (string.IsNullOrWhiteSpace(imagePath))
                throw new ArgumentException("Image path is required.", nameof(imagePath));

            if (string.IsNullOrWhiteSpace(destPath))
                throw new ArgumentException("Destination path is required.", nameof(destPath));

            if (quality < 0 || quality > 100)
                throw new ArgumentOutOfRangeException(nameof(quality), "Quality must be between 0 and 100.");

            if (!File.Exists(imagePath))
                throw new FileNotFoundException($"Image not found: {imagePath}");

            using var inputStream = File.OpenRead(imagePath);
            using var originalBitmap = SKBitmap.Decode(inputStream);

            if (originalBitmap == null)
                throw new InvalidDataException($"Failed to decode image: {imagePath}");

            using var image = SKImage.FromBitmap(originalBitmap);
            using var data = image.Encode(SKEncodedImageFormat.Jpeg, (int)quality);

            var destFolder = Path.GetDirectoryName(destPath);
            if (!string.IsNullOrEmpty(destFolder) && !Directory.Exists(destFolder))
                Directory.CreateDirectory(destFolder);

            using var outputStream = File.OpenWrite(destPath);
            data.SaveTo(outputStream);
        }

        public static bool IsValidImage(string imagePath)
        {
            if (string.IsNullOrWhiteSpace(imagePath) || !File.Exists(imagePath))
                return false;

            try
            {
                using var stream = File.OpenRead(imagePath);
                using var bitmap = SKBitmap.Decode(stream);
                return bitmap != null;
            }
            catch
            {
                return false;
            }
        }

        public static (int Width, int Height) GetImageDimensions(string imagePath)
        {
            if (string.IsNullOrWhiteSpace(imagePath))
                throw new ArgumentException("Image path is required.", nameof(imagePath));

            if (!File.Exists(imagePath))
                throw new FileNotFoundException($"Image not found: {imagePath}");

            using var stream = File.OpenRead(imagePath);
            using var bitmap = SKBitmap.Decode(stream);

            if (bitmap == null)
                throw new InvalidDataException($"Failed to decode image: {imagePath}");

            return (bitmap.Width, bitmap.Height);
        }
    }
}