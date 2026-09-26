using Common.Aplication;
using Common.Application;
using Shop.Domain.OrderAgg.Repository;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Application.Order.RemoveDiscount
{
    public record RemoveOrderDiscountCommand(Guid UserId) : IBaseCommand;
}