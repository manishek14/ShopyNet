using Common.Aplication;
using Common.Application;
using Common.Domain.ValueObject;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Application.Products.Create
{
    public record CreateProductCommand(
        string Title,
        string Description,
        IFormFile ImageFile,
        Guid CategoryId,
        Guid SubCategoryId,
        Guid SecondarySubCategoryId,
        string Slug,
        SeoData SeoData,
        Dictionary<string, string> Specifications
    ) : IBaseCommand;
}