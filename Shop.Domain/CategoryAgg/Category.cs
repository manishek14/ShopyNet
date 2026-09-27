using Common.Domain;
using Common.Domain.ValueObject;
using Shop.Domain.UserAgg;
using System;
using System.Collections.Generic;

namespace Shop.Domain.CategoryAgg
{
    public class Category : BaseAggregate
    {
        // EF Core
        protected Category() { }

        public Category(string title, string slug, SeoData seoData)
        {
            Guard(title, slug, seoData);

            Title = title;
            Slug = string.IsNullOrWhiteSpace(slug)
                ? Slugifier.Slugify(title)
                : Slugifier.Slugify(slug);
            SeoData = seoData;
            Childs = new List<Category>();
        }

        public string Title { get; private set; } = string.Empty;
        public string Slug { get; private set; } = string.Empty;
        public SeoData SeoData { get; private set; } = null!;
        public Guid? ParentId { get; private set; }
        public List<Category> Childs { get; private set; } = new();

        public void Edit(string title, string slug, SeoData seoData)
        {
            Guard(title, slug, seoData);

            Title = title;
            Slug = string.IsNullOrWhiteSpace(slug)
                ? Slugifier.Slugify(title)
                : Slugifier.Slugify(slug);
            SeoData = seoData;
        }

        public void AddChild(string title, string slug, SeoData seoData)
        {
            Childs.Add(new Category(title, slug, seoData)
            {
                ParentId = Id
            });
        }

        private static void Guard(string title, string slug, SeoData seoData)
        {
            NullOrEmptyDomainDataException.CheckString(title, nameof(title));
            NullOrEmptyDomainDataException.CheckString(slug, nameof(slug));

            if (seoData == null)
                throw new NullOrEmptyDomainDataException("SeoData cannot be null.");
        }
    }
}