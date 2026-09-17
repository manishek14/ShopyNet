using System;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Common.Domain
{
    public static class Slugifier
    {
        public static string Slugify(string? input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;

            string normalized = input.Normalize(NormalizationForm.FormD);
            var sb = new StringBuilder();

            foreach (var c in normalized)
            {
                var uc = CharUnicodeInfo.GetUnicodeCategory(c);
                if (uc == UnicodeCategory.NonSpacingMark)
                    continue;

                sb.Append(c);
            }

            string cleaned = sb.ToString().Normalize(NormalizationForm.FormC);

            cleaned = cleaned.ToLowerInvariant();

            cleaned = Regex.Replace(cleaned, @"[^a-z0-9\s-]", string.Empty);

            cleaned = Regex.Replace(cleaned, @"[\s]+", "-");

            cleaned = Regex.Replace(cleaned, "-+", "-");

            return cleaned.Trim('-');
        }
    }
}
