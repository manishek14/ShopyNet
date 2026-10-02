using Common.Query;
using System;

namespace Shop.Query.User.DTOs
{
    public class UserTokenDto : BaseDto
    {
        public Guid UserId { get; set; }
        public string HashJwtToken { get; set; } = string.Empty;
        public string HashRefreshToken { get; set; } = string.Empty;
        public DateTime TokenExpireDate { get; set; }
        public DateTime RefreshTokenExpireDate { get; set; }
        public string Device { get; set; } = string.Empty;
    }
}