using Shop.Query.User.DTOs;

namespace Shop.Query.User.GetUserTokenByJwtToken
{
    public record GetUserTokenByJwtTokenQuery(string HashJwtToken)
        : IBaseQuery<UserTokenDto>;
}