using Common.Aplication;
using Common.Application;
using Shop.Domain.SellerAgg;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Application.Sellers.RemoveInventory
{
    public record RemoveSellerInventoryCommand(Guid SellerId, Guid ProductId) : IBaseCommand;
}