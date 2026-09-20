using Common.Domain;
using Common.Domain.ValueObject;
using Shop.Domain.UserAgg;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Shop.Domain.ProductAgg
{
    public class Product : BaseAggregate
    {
        private Product() { }

        public Product(
            string title,
            string imageName,
            string description,
            Guid categoryId,
            Guid subCategoryId,
            Guid nestedCategoryId,
            string slug,
            SeoData seoData)
        {
            Guard(title, imageName, description, categoryId, subCategoryId, nestedCategoryId);

            Title = title;
            ImageName = imageName;
            Description = description;
            CategoryId = categoryId;
            SubCategoryId = subCategoryId;
            NestedCategoryId = nestedCategoryId;
            Slug = string.IsNullOrWhiteSpace(slug)
                ? Slugifier.Slugify(title)
                : Slugifier.Slugify(slug);
            SeoData = seoData;

            Images = new List<ProductImage>();
            Specifications = new List<ProductSpecification>();

            CreatedAt = DateTime.Now;
        }

        public string Title { get; private set; } = string.Empty;
        public string ImageName { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public Guid CategoryId { get; private set; }
        public Guid SubCategoryId { get; private set; }
        public Guid NestedCategoryId { get; private set; }
        public string Slug { get; private set; } = string.Empty;
        public SeoData SeoData { get; private set; }
        public List<ProductImage> Images { get; private set; }
        public List<ProductSpecification> Specifications { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }
        public bool IsActive { get; private set; } = true;

        public void Edit(
            string title,
            string imageName,
            string description,
            Guid categoryId,
            Guid subCategoryId,
            Guid nestedCategoryId,
            string slug,
            SeoData seoData)
        {
            Guard(title, imageName, description, categoryId, subCategoryId, nestedCategoryId);

            Title = title;
            ImageName = imageName;
            Description = description;
            CategoryId = categoryId;
            SubCategoryId = subCategoryId;
            NestedCategoryId = nestedCategoryId;
            Slug = string.IsNullOrWhiteSpace(slug)
                ? Slugifier.Slugify(title)
                : Slugifier.Slugify(slug);
            SeoData = seoData;
            UpdatedAt = DateTime.Now;
        }

        public void SetSpecification(List<ProductSpecification> specifications)
        {
            if (specifications == null)
                throw new ArgumentNullException(nameof(specifications));

            Specifications.Clear();
            Specifications.AddRange(specifications);
            UpdatedAt = DateTime.Now;
        }

        public void AddImage(ProductImage image)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));

            image.ProductId = Id;
            Images.Add(image);
            UpdatedAt = DateTime.Now;
        }

        public void RemoveImage(Guid imageId)
        {
            var image = Images.FirstOrDefault(i => i.Id == imageId);
            if (image == null)
                throw new KeyNotFoundException($"Image with ID {imageId} not found.");

            Images.Remove(image);
            UpdatedAt = DateTime.Now;
        }

        public void Activate()
        {
            IsActive = true;
            UpdatedAt = DateTime.Now;
        }

        public void Deactivate()
        {
            IsActive = false;
            UpdatedAt = DateTime.Now;
        }

        private static void Guard(
            string title,
            string imageName,
            string description,
            Guid categoryId,
            Guid subCategoryId,
            Guid nestedCategoryId)
        {
            NullOrEmptyDomainDataException.CheckString(title, nameof(title));
            NullOrEmptyDomainDataException.CheckString(imageName, nameof(imageName));
            NullOrEmptyDomainDataException.CheckString(description, nameof(description));

            if (categoryId == Guid.Empty)
                throw new ArgumentException("CategoryId cannot be empty.", nameof(categoryId));

            if (subCategoryId == Guid.Empty)
                throw new ArgumentException("SubCategoryId cannot be empty.", nameof(subCategoryId));

            if (nestedCategoryId == Guid.Empty)
                throw new ArgumentException("NestedCategoryId cannot be empty.", nameof(nestedCategoryId));
        }
    }
}