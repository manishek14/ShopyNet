using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.ProductAgg;

namespace Shop.Infrastructure.Persistent.Ef.ProductAgg
{
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products", "Product");
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Title).HasMaxLength(200).IsRequired();
            builder.Property(p => p.ImageName).HasMaxLength(500).IsRequired();
            builder.Property(p => p.Description).HasMaxLength(5000).IsRequired();
            builder.Property(p => p.CategoryId).IsRequired();
            builder.Property(p => p.SubCategoryId).IsRequired();
            builder.Property(p => p.NestedCategoryId).IsRequired();
            builder.Property(p => p.Slug).HasMaxLength(200).IsRequired();

            builder.OwnsOne(p => p.SeoData, seo =>
            {
                seo.Property(s => s.MetaData)
                    .HasColumnName("MetaTitle")      
                    .HasMaxLength(60)
                    .IsRequired(false);

                seo.Property(s => s.MetaDescription)
                    .HasColumnName("MetaDescription")    
                    .HasMaxLength(160)
                    .IsRequired(false);

                seo.Property(s => s.MetaKeywords)
                    .HasColumnName("Keywords")         
                    .HasMaxLength(500)
                    .IsRequired(false);

                seo.Property(s => s.IndexPage)
                    .HasColumnName("SeoData_IndexPage")   
                    .IsRequired();

                seo.Property(s => s.CanonicalUrl)
                    .HasColumnName("Canonical")           
                    .HasMaxLength(500)
                    .IsRequired(false);

                seo.Property(s => s.Schema)
                    .HasColumnName("SeoData_Schema")      
                    .HasMaxLength(2000)
                    .IsRequired(false);
            });

            builder.HasMany(p => p.Images)
                .WithOne()
                .HasForeignKey(pi => pi.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.Specifications)
                .WithOne()
                .HasForeignKey(ps => ps.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(p => p.IsActive).IsRequired().HasDefaultValue(true);

            builder.HasIndex(p => p.Slug).IsUnique().HasDatabaseName("IX_Products_Slug");
            builder.HasIndex(p => p.CategoryId).HasDatabaseName("IX_Products_CategoryId");
            builder.HasIndex(p => p.IsActive).HasDatabaseName("IX_Products_IsActive");
        }
    }
}