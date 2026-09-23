using Common.Aplication;
using Common.Application;
using Common.Domain.ValueObject;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Application.Category.Edit
{
    public record EditCategoryCommand(Guid Id, string Title, string Slug, SeoData SeoData) : IBaseCommand;
}