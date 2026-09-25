using Common.Aplication;
using Common.Application;
using Common.Domain.ValueObject;
using Microsoft.AspNetCore.Http;
using Shop.Domain.ProductAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Application.Products.Edit
{
    public record EditProductCommand(
        Guid Id,
        string Title,
        string Description,
        IFormFile? ImageFile,
        Guid CategoryId,
        Guid SubCategoryId,
        Guid SecondarySubCategoryId,
        string Slug,
        SeoData SeoData,
        Dictionary<string, string> Specifications
    ) : IBaseCommand;
}