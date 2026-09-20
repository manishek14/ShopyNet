using Common.Aplication;
using Common.Application;
using Common.Domain.ValueObject;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Application.Category.AddChild
{
    public record AddChildCategoryCommand(Guid ParentId, string Title, string Slug, SeoData SeoData) : IBaseCommand;
}