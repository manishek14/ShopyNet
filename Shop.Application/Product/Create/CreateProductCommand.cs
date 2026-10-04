using Common.Aplication;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Common.Domain.ValueObject;
using System;
using System.Collections.Generic;

namespace Shop.Application.Products.Create
{
    public record SpecificationDto(string Key, string Value);

    public record CreateProductCommand(
        string Title,
        string Description,
        IFormFile ImageFile,
        Guid CategoryId,
        Guid SubCategoryId,
        Guid SecondarySubCategoryId,
        string Slug,
        SeoData? SeoData,
        List<SpecificationDto>? Specifications
    ) : IBaseCommand;

    // Validator is defined in CreateProductCommandValidator.cs
}