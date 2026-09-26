using Common.Aplication;
using Common.Application;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Application.Order.IncreaseItemCount
{
    public record IncreaseOrderItemCountCommand(
        Guid UserId,
        Guid InventoryId,
        int Count
    ) : IBaseCommand;
}