using Common.Aplication;
using Common.Application;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Application.Role.Delete
{
    public record DeleteRoleCommand(Guid Id) : IBaseCommand;
}