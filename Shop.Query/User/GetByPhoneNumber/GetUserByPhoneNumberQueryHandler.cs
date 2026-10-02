using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.User.DTOs;
using System.Linq;
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
                .Include(u => u.UserRoles)
                .Include(u => u.UserAddresses)
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.PhoneNumber == request.PhoneNumber, cancellationToken);

            if (user == null)
                return null;

            var userDto = user.Map();

            if (userDto != null && userDto.UserRoles.Any())
            {
                var roleIds = userDto.UserRoles.Select(ur => ur.RoleId).ToList();

                var roles = await _shopContext.Roles
                    .Where(r => roleIds.Contains(r.Id))
                    .Select(r => new { r.Id, r.Title })
                    .ToListAsync(cancellationToken);

                foreach (var userRole in userDto.UserRoles)
                {
                    var role = roles.FirstOrDefault(r => r.Id == userRole.RoleId);
                    if (role != null)
                        userRole.RoleTitle = role.Title;
                }

                userDto.Roles = userDto.UserRoles;
            }

            return userDto;
        }
    }
}