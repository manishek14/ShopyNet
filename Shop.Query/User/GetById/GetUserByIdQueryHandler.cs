using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.User.DTOs;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Query.User.GetById
{
    internal class GetUserByIdQueryHandler : IBaseQueryHandler<GetUserByIdQuery, UserDto>
    {
        private readonly ShopContext _shopContext;

        public GetUserByIdQueryHandler(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _shopContext.Users
                .Include(u => u.UserRoles)
                .Include(u => u.UserAddresses)
                .SingleOrDefaultAsync(u => u.Id == request.Id, cancellationToken);

            return user.Map();
        }
    }
}