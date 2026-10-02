using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.User.DTOs;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Query.User.GetUserTokenByRefreshToken
{
    internal class GetUserTokenByRefreshTokenQueryHandler
        : IBaseQueryHandler<GetUserTokenByRefreshTokenQuery, UserTokenDto>
    {
        private readonly ShopContext _shopContext;

        public GetUserTokenByRefreshTokenQueryHandler(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        public async Task<UserTokenDto> Handle(
            GetUserTokenByRefreshTokenQuery request,
            CancellationToken cancellationToken)
        {
            var token = await _shopContext.UserTokens
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.HashRefreshToken == request.HashRefreshToken, cancellationToken);

            return token?.Map();
        }
    }
}