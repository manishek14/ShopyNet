using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.CategoryAgg;
using Shop.Domain.Shared;

namespace Shop.Infrastructure.Persistent.Ef.CategoryAgg
{
    internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories", "Category");
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Title)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(c => c.Slug)
                .HasMaxLength(200)
                .IsRequired();

            builder.OwnsOne(c => c.SeoData, seo =>
            {
                seo.Property(s => s.MetaData)
                    .HasColumnName("MetaTitle")
                    .HasMaxLength(60)
                    .IsRequired(false);

                seo.Property(s => s.MetaDescription)
                    .HasColumnName("MetaDescription")
                    .HasMaxLength(160)
                    .IsRequired(false);

                seo.Property(s => s.CanonicalUrl)
                    .HasColumnName("Canonical")
                    .HasMaxLength(500)
                    .IsRequired(false);

                seo.Property(s => s.MetaKeywords)
                    .HasColumnName("Keywords")
                    .HasMaxLength(500)
                    .IsRequired(false);
            });

            builder.Property(c => c.ParentID)
                .IsRequired(false);

            builder.HasOne<Category>()
                .WithMany()
                .HasForeignKey(c => c.ParentID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.Childs)
                .WithOne()
                .HasForeignKey(c => c.ParentID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(c => c.Slug)
                .IsUnique()
                .HasDatabaseName("IX_Categories_Slug");

            builder.HasIndex(c => c.Title)
                .HasDatabaseName("IX_Categories_Title");

            builder.HasIndex(c => c.ParentID)
                .HasDatabaseName("IX_Categories_ParentId");
        }
    }
}