using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.User.DTOs;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Query.User.GetByPhoneNumber
{
    internal class GetUserByPhoneNumberQueryHandler
        : IBaseQueryHandler<GetUserByPhoneNumberQuery, UserDto>
    {
        private readonly ShopContext _shopContext;

        public GetUserByPhoneNumberQueryHandler(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        public async Task<UserDto> Handle(
            GetUserByPhoneNumberQuery request,
            CancellationToken cancellationToken)
        {
            var user = await _shopContext.Users
                .SingleOrDefaultAsync(u => u.PhoneNumber == request.PhoneNumber, cancellationToken);

            return user.Map();
        }
    }
}