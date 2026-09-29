using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.SellerAgg;

namespace Shop.Infrastructure.Persistent.Ef.SellerAgg
{
    internal class SellerInventoryConfiguration : IEntityTypeConfiguration<SellerInventory>
    {
        public void Configure(EntityTypeBuilder<SellerInventory> builder)
        {
            builder.ToTable("SellerInventories", "Seller");
            builder.HasKey(si => si.Id);

            builder.Property(si => si.SellerId).IsRequired();
            builder.Property(si => si.ProductId).IsRequired();
            builder.Property(si => si.Count).IsRequired();
            builder.Property(si => si.Price).IsRequired();

            builder.HasIndex(si => si.SellerId)
                .HasDatabaseName("IX_SellerInventories_SellerId");

            builder.HasIndex(si => si.ProductId)
                .HasDatabaseName("IX_SellerInventories_ProductId");

            builder.HasIndex(si => new { si.SellerId, si.ProductId })
                .IsUnique()
                .HasDatabaseName("IX_SellerInventories_SellerId_ProductId");
        }
    }
}