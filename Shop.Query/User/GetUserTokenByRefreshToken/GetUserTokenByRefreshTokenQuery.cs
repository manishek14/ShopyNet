using Shop.Query.User.DTOs;

namespace Shop.Query.User.GetUserTokenByRefreshToken
{
    public record GetUserTokenByRefreshTokenQuery(string HashRefreshToken)
        : IBaseQuery<UserTokenDto>;
}