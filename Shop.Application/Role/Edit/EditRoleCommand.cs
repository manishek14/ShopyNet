using Common.Aplication;
using Common.Application;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Application.Role.Edit
{
    public record EditRoleCommand(
        Guid Id,
        string Title
    ) : IBaseCommand;
}