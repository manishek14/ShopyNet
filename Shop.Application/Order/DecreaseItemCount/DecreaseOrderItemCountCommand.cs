using Common.Aplication;
using Common.Application;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Application.Order.DecreaseItemCount
{
    public record DecreaseOrderItemCountCommand(
        Guid UserId,
        Guid InventoryId,
        int Count
    ) : IBaseCommand;
}