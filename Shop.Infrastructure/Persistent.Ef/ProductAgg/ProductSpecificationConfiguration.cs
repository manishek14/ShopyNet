using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.ProductAgg;

namespace Shop.Infrastructure.Persistent.Ef.ProductAgg
{
    internal class ProductSpecificationConfiguration : IEntityTypeConfiguration<ProductSpecification>
    {
        public void Configure(EntityTypeBuilder<ProductSpecification> builder)
        {
            builder.ToTable("ProductSpecifications", "Product");
            builder.HasKey(ps => ps.Id);

            builder.Property(ps => ps.ProductId).IsRequired();

            builder.Property(ps => ps.Key)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(ps => ps.Value)
                .HasMaxLength(1000)
                .IsRequired();

            builder.HasIndex(ps => ps.ProductId)
                .HasDatabaseName("IX_ProductSpecifications_ProductId");

            builder.HasIndex(ps => new { ps.ProductId, ps.Key })
                .HasDatabaseName("IX_ProductSpecifications_ProductId_Key");
        }
    }
}