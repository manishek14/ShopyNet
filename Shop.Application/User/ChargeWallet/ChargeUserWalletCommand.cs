using Common.Aplication;
using Common.Application;
using Shop.Domain.UserAgg.Enums;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Application.User.ChargeWallet
{
    public record ChargeUserWalletCommand(Guid UserId, int Amount, string Description, WalletType Type) : IBaseCommand;
}