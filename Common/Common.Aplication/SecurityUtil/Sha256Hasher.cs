using System.Security.Cryptography;
using System.Text;

namespace Common.Aplication.SecurityUtil
{
    public static class Sha256Hasher
    {
        public static string Hash(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;

            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(input);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        public static bool IsCompare(string hash, string input)
        {
            return Hash(input) == hash;
        }
    }
}