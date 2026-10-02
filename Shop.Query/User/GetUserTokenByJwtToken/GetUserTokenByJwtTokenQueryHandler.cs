using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.User.DTOs;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Query.User.GetUserTokenByJwtToken
{
    internal class GetUserTokenByJwtTokenQueryHandler
        : IBaseQueryHandler<GetUserTokenByJwtTokenQuery, UserTokenDto>
    {
        private readonly ShopContext _shopContext;

        public GetUserTokenByJwtTokenQueryHandler(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        public async Task<UserTokenDto> Handle(
    GetUserTokenByJwtTokenQuery request,
    CancellationToken cancellationToken)
        {
            var token = await _shopContext.UserTokens
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.HashJwtToken == request.HashJwtToken, cancellationToken);

            return token?.Map();
        }
    }
}