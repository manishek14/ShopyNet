using Common.Domain;
using Common.Domain.ValueObject;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Domain.CategoryAgg
{
    public class Category : BaseAggregate
    {
        public Category(string title, string slug, SeoData seoData)
        {
            GuardForNullOrEmpty();
            Title = title;
            Slug = string.IsNullOrWhiteSpace(slug) ? Slugifier.Slugify(title) : Slugifier.Slugify(slug);
            SeoData = seoData;
        }

        public string Title { get; private set; }
        public string Slug { get; private set; }
        public SeoData SeoData { get; private set; }
        public Guid? ParentID { get; private set; }
        public List<Category> Childs { get; private set; }

        public void Edit(string title, string slug, SeoData seoData)
        {
            Title = title;
            Slug = string.IsNullOrWhiteSpace(slug) ? Slugifier.Slugify(title) : Slugifier.Slugify(slug);
            SeoData = seoData;
        }

        public void AddChild(string title, string slug, SeoData seoData)
        {
            Childs.Add(new Category(title, slug, seoData) {
                ParentID = Id 
            });
        }

        public void GuardForNullOrEmpty()
        {
            if (string.IsNullOrEmpty(Title))
                throw new ArgumentException("Title cannot be null or empty!");
            if (string.IsNullOrEmpty(Slug))
                throw new ArgumentException("Slug cannot be null or empty!");
            if (SeoData == null)
                throw new ArgumentException("SeoData cannot be null!");
        }
    }
}
