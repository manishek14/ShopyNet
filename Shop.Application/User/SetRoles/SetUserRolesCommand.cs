using Common.Aplication;
using Common.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Application.User.SetRoles
{
    public record SetUserRolesCommand(
        Guid UserId,
        List<Guid> RoleIds
    ) : IBaseCommand;

}