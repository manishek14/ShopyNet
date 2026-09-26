using Common.Aplication;
using Common.Application;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Application.Order.AddItem
{
    public record AddOrderItemCommand(
        Guid UserId,
        Guid InventoryId,
        int Count,
        int Price
    ) : IBaseCommand;
}