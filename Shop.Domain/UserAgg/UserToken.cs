using Common.Domain;
using System;

namespace Shop.Domain.UserAgg
{
    public class UserToken : BaseEntity
    {
        // EF Core
        private UserToken() { }

        public UserToken(
            Guid userId,
            string hashJwtToken,
            string hashRefreshToken,
            DateTime tokenExpireDate,
            DateTime refreshTokenExpireDate,
            string device)
        {
            if (userId == Guid.Empty)
                throw new ArgumentException("UserId cannot be empty.", nameof(userId));

            NullOrEmptyDomainDataException.CheckString(hashJwtToken, nameof(hashJwtToken));
            NullOrEmptyDomainDataException.CheckString(hashRefreshToken, nameof(hashRefreshToken));

            UserId = userId;
            HashJwtToken = hashJwtToken;
            HashRefreshToken = hashRefreshToken;
            TokenExpireDate = tokenExpireDate;
            RefreshTokenExpireDate = refreshTokenExpireDate;
            Device = device;
            CreatedAt = DateTime.Now;
        }

        public Guid UserId { get; private set; }
        public string HashJwtToken { get; private set; } = string.Empty;
        public string HashRefreshToken { get; private set; } = string.Empty;
        public DateTime TokenExpireDate { get; private set; }
        public DateTime RefreshTokenExpireDate { get; private set; }
        public string Device { get; private set; } = string.Empty;
        public DateTime CreatedAt { get; private set; }

        public void UpdateRefreshToken(
            string newHashJwtToken,
            string newHashRefreshToken,
            DateTime newTokenExpireDate,
            DateTime newRefreshTokenExpireDate)
        {
            NullOrEmptyDomainDataException.CheckString(newHashJwtToken, nameof(newHashJwtToken));
            NullOrEmptyDomainDataException.CheckString(newHashRefreshToken, nameof(newHashRefreshToken));

            HashJwtToken = newHashJwtToken;
            HashRefreshToken = newHashRefreshToken;
            TokenExpireDate = newTokenExpireDate;
            RefreshTokenExpireDate = newRefreshTokenExpireDate;
        }

        public bool IsTokenExpired()
        {
            return TokenExpireDate < DateTime.Now;
        }

        public bool IsRefreshTokenExpired()
        {
            return RefreshTokenExpireDate < DateTime.Now;
        }

        public bool IsValid()
        {
            return !IsTokenExpired() && !IsRefreshTokenExpired();
        }
    }
}