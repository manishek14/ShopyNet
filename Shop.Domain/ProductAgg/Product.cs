using Common.Domain;
using Common.Domain.ValueObject;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Domain.ProductAgg
{
    public class Product : BaseAggregate
    {
        public Product(string title, string imageName, string description, Guid categoryId, Guid subCategoryId, Guid nestedCategoryId, string slug, SeoData seoData)
        {
            Title = title;
            ImageName = imageName;
            Description = description;
            CategoryId = categoryId;
            SubCategoryId = subCategoryId;
            NestedCategoryId = nestedCategoryId;
            Slug = string.IsNullOrWhiteSpace(slug) ? Slugifier.Slugify(title) : Slugifier.Slugify(slug);
            SeoData = seoData;
        }

        public string Title { get; private set; }
        public string ImageName { get; private set; }
        public string Description { get; private set; }
        public Guid CategoryId { get; private set; }
        public Guid SubCategoryId { get; private set; }
        public Guid NestedCategoryId { get; private set; } //Sub-SubCategory
        public string Slug { get; private set; }
        public SeoData SeoData { get; private set; }
        public List<ProductImage> Images { get; private set; }
        public List<ProductSpecification> Specifications { get; private set; }  

        public void Edit(string title, string imageName, string description, Guid categoryId, Guid subCategoryId, Guid nestedCategoryId, string slug, SeoData seoData)
        {
            GuardAgainstNullOrEmpty();
            Title = title;
            ImageName = imageName;
            Description = description;
            CategoryId = categoryId;
            SubCategoryId = subCategoryId;
            NestedCategoryId = nestedCategoryId;
            Slug = string.IsNullOrWhiteSpace(slug) ? Slugifier.Slugify(title) : Slugifier.Slugify(slug);
            SeoData = seoData;
        }

        public void AddImage(ProductImage image)
        {
            if (Images == null)
                Images = new List<ProductImage>();
            Images.Add(image);
        }
        public void RemoveImage(ProductImage image)
        {
            if (Images != null)
                Images.Remove(image);
        }

        public void AddSpecification(List<ProductSpecification> specifications)
        {
            if (Specifications == null)
                Specifications = new List<ProductSpecification>();
            specifications.ForEach(spec => Specifications.Add(spec));
        }
        public void GuardAgainstNullOrEmpty()
        {
            if (string.IsNullOrEmpty(Title))
                throw new ArgumentException("Title cannot be null or empty!");
            if (string.IsNullOrEmpty(ImageName))
                throw new ArgumentException("ImageName cannot be null or empty!");
            if (string.IsNullOrEmpty(Description))
                throw new ArgumentException("Description cannot be null or empty!");
            if (CategoryId == Guid.Empty)
                throw new ArgumentException("CategoryId cannot be empty!");
            if (SubCategoryId == Guid.Empty)
                throw new ArgumentException("SubCategoryId cannot be empty!");
            if (NestedCategoryId == Guid.Empty)
                throw new ArgumentException("NestedCategoryId cannot be empty!");
            if (string.IsNullOrEmpty(Slug))
                throw new ArgumentException("Slug cannot be null or empty!");
        }
    }
}
