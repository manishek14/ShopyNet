using Common.Aplication;
using Common.Application;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Application.Sellers.Edit
{
    public record EditSellerCommand(Guid Id, string ShopName, string NationalCode) : IBaseCommand;
}