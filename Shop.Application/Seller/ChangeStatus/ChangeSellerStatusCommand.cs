using Common.Aplication;
using Common.Application;
using Shop.Domain.SellerAgg.Enums;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Application.Sellers.ChangeStatus
{
    public record ChangeSellerStatusCommand(Guid Id, SellerStatus Status) : IBaseCommand;
}