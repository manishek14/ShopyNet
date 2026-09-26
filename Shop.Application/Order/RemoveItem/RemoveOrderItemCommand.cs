using Clean_Arch.Query.Shared.Repository;
using Common.Aplication;
using Common.Application;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Application.Order.RemoveItem
{
    public record RemoveOrderItemCommand(
        Guid UserId,
        Guid InventoryId
    ) : IBaseCommand;
}