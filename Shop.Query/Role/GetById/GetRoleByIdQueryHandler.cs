using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.Role.DTOs;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Query.Role.GetById
{
    internal class GetRoleByIdQueryHandler : IBaseQueryHandler<GetRoleByIdQuery, RoleDto>
    {
        private readonly ShopContext _shopContext;

        public GetRoleByIdQueryHandler(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        public async Task<RoleDto> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
            var role = await _shopContext.Roles
                .Include(r => r.Permissions)
                .SingleOrDefaultAsync(r => r.Id == request.Id, cancellationToken);

            return role.Map();
        }
    }
}