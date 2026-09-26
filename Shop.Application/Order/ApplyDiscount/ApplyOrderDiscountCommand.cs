using Common.Aplication;
using Common.Application;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Application.Order.ApplyDiscount
{
    public record ApplyOrderDiscountCommand(
        Guid UserId,
        string DiscountTitle,
        int DiscountAmount
    ) : IBaseCommand;
}