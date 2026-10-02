namespace Shop.Api.Infrastructure.JwtUtil
{
    public class JwtConfig
    {
        public string SignInKey { get; set; } = string.Empty;
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public int TokenExpireDays { get; set; } = 7;
        public int RefreshTokenExpireDays { get; set; } = 8;
    }
}