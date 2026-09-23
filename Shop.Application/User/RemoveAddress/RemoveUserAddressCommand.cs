using Common.Aplication;
using Common.Application;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Application.User.RemoveAddress
{
    public record RemoveUserAddressCommand(Guid UserId, Guid AddressId) : IBaseCommand;
}