using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.ProductAgg;

namespace Shop.Infrastructure.Persistent.Ef.ProductAgg
{
    internal class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
    {
        public void Configure(EntityTypeBuilder<ProductImage> builder)
        {
            builder.ToTable("ProductImages", "Product");
            builder.HasKey(pi => pi.Id);

            builder.Property(pi => pi.ProductId).IsRequired();

            builder.Property(pi => pi.ImageName)
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(pi => pi.Sequence)
                .HasMaxLength(50)
                .IsRequired();

            builder.HasIndex(pi => pi.ProductId)
                .HasDatabaseName("IX_ProductImages_ProductId");

            builder.HasIndex(pi => new { pi.ProductId, pi.Sequence })
                .HasDatabaseName("IX_ProductImages_ProductId_Sequence");
        }
    }
}