using Common.Aplication;
using Common.Application;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Application.Products.RemoveImage
{
    public record RemoveProductImageCommand(Guid ProductId, Guid ImageId) : IBaseCommand;
}