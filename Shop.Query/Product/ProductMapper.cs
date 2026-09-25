using Shop.Query.Product.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace Shop.Query.Product
{
    internal static class ProductMapper
    {
        public static ProductDto? Map(this Domain.ProductAgg.Product? product)
        {
            if (product is null)
                return null;

            return new ProductDto
            {
                Id = product.Id,
                Title = product.Title,
                ImageName = product.ImageName,
                Description = product.Description,
                CategoryId = product.CategoryId,
                SubCategoryId = product.SubCategoryId,
                NestedCategoryId = product.NestedCategoryId,
                Slug = product.Slug,
                SeoData = product.SeoData,
                IsActive = product.IsActive,
                CreatedDate = product.CreatedAt,
                UpdatedDate = product.UpdatedAt,
                Images = product.Images?.Select(MapImage).ToList() ?? new List<ProductImageDto>(),
                Specifications = product.Specifications?.Select(MapSpec).ToList() ?? new List<ProductSpecificationDto>()
            };
        }

        public static List<ProductDto> MapList(this List<Domain.ProductAgg.Product>? products)
        {
            var result = new List<ProductDto>();
            if (products == null)
                return result;

            foreach (var product in products)
            {
                result.Add(new ProductDto
                {
                    Id = product.Id,
                    Title = product.Title,
                    ImageName = product.ImageName,
                    Description = product.Description,
                    CategoryId = product.CategoryId,
                    SubCategoryId = product.SubCategoryId,
                    NestedCategoryId = product.NestedCategoryId,
                    Slug = product.Slug,
                    SeoData = product.SeoData,
                    IsActive = product.IsActive,
                    CreatedDate = product.CreatedAt,
                    UpdatedDate = product.UpdatedAt,
                    Images = new List<ProductImageDto>(),
                    Specifications = new List<ProductSpecificationDto>()
                });
            }

            return result;
        }

        private static ProductImageDto MapImage(Domain.ProductAgg.ProductImage image)
        {
            return new ProductImageDto
            {
                Id = image.Id,
                ProductId = image.ProductId,
                ImageName = image.ImageName,
                Sequence = image.Sequence
            };
        }

        private static ProductSpecificationDto MapSpec(Domain.ProductAgg.ProductSpecification spec)
        {
            return new ProductSpecificationDto
            {
                Id = spec.Id,
                ProductId = spec.ProductId,
                Key = spec.Key,
                Value = spec.Value
            };
        }
    }
}