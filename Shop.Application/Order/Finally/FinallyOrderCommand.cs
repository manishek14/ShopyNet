using Common.Aplication;
using Common.Application;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Application.Order.Finally
{
    public record FinallyOrderCommand(Guid UserId) : IBaseCommand;
}