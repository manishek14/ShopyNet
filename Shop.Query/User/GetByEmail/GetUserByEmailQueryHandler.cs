using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.User.DTOs;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Query.User.GetByEmail
{
    internal class GetUserByEmailQueryHandler : IBaseQueryHandler<GetUserByEmailQuery, UserDto>
    {
        private readonly ShopContext _shopContext;

        public GetUserByEmailQueryHandler(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        public async Task<UserDto> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
        {
            var user = await _shopContext.Users
                .SingleOrDefaultAsync(u => u.Email == request.Email, cancellationToken);

            return user.Map();
        }
    }
}