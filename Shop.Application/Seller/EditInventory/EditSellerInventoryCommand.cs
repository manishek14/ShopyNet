using Common.Aplication;
using Common.Application;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Application.Sellers.EditInventory
{
    public record EditSellerInventoryCommand(Guid SellerId, Guid ProductId, int Count, int Price) : IBaseCommand;
}