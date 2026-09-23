using Common.Aplication;
using Common.Application;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Application.Sellers.Create
{
    public record CreateSellerCommand(Guid UserId, string ShopName, string NationalCode) : IBaseCommand;
}