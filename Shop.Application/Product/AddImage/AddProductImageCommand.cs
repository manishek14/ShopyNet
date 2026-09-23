using Common.Aplication;
using Common.Application;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Application.Products.AddImage
{
    public record AddProductImageCommand(Guid ProductId, IFormFile ImageFile, string Sequence) : IBaseCommand;
}