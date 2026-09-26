using Common.Aplication;
using Common.Application;
using Shop.Domain.OrderAgg;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Application.Order.SetShippingMethod
{
    public record SetOrderShippingMethodCommand(
        Guid UserId,
        string ShippingType,
        int ShippingCost
    ) : IBaseCommand;
}