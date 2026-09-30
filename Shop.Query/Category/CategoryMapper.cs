using Shop.Query.Category.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Shop.Query.Category
{
    internal static class CategoryMapper
    {
        public static CategoryDto? Map(this Domain.CategoryAgg.Category? category)
        {
            if (category is null)
                return null;

            return new CategoryDto
            {
                Id = category.Id,
                Title = category.Title,
                Slug = category.Slug,
                SeoData = category.SeoData,
                ParentID = category.ParentId,
                CreatedDate = category.CreationDate
            };
        }

        public static List<CategoryDto> MapList(this List<Domain.CategoryAgg.Category>? categories)
        {
            var result = new List<CategoryDto>();
            if (categories == null)
                return result;

            foreach (var category in categories)
            {
                result.Add(Map(category)!);
            }

            return result;
        }

        public static List<CategoryWithChildsDto> SubMap(this List<Domain.CategoryAgg.Category>? categories)
        {
            var result = new List<CategoryWithChildsDto>();
            if (categories == null)
                return result;

            foreach (var category in categories)
            {
                result.Add(new CategoryWithChildsDto
                {
                    Id = category.Id,
                    Title = category.Title,
                    Slug = category.Slug,
                    SeoData = category.SeoData,
                    ParentID = category.ParentId,
                    CreatedDate = category.CreationDate,
                    Childs = category.Childs?
                        .Select(c => Map(c))
                        .Where(d => d != null)
                        .Select(d => d!)
                        .ToList() ?? new List<CategoryDto>()
                });
            }

            return result;
        }

        public static List<CategoryWithParentDto> SubParentMap(this List<Domain.CategoryAgg.Category>? categories)
        {
            var result = new List<CategoryWithParentDto>();
            if (categories == null)
                return result;

            foreach (var category in categories)
            {
                result.Add(new CategoryWithParentDto
                {
                    Id = category.Id,
                    Title = category.Title,
                    Slug = category.Slug,
                    SeoData = category.SeoData,
                    ParentID = category.ParentId,
                    CreatedDate = category.CreationDate
                });
            }

            return result;
        }
    }
}