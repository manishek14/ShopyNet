using Common.Aplication;
using Common.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Application.Role.Create
{
    public record CreateRoleCommand(
        string Title,
        List<Shop.Domain.RoleAgg.Role.Permission> Permissions
    ) : IBaseCommand;
}