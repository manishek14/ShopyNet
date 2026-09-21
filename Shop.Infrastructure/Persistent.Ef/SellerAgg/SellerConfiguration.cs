using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.SellerAgg;

namespace Shop.Infrastructure.Persistent.Ef.SellerAgg
{
    internal class SellerConfiguration : IEntityTypeConfiguration<Seller>
    {
        public void Configure(EntityTypeBuilder<Seller> builder)
        {
            builder.ToTable("Sellers", "Seller");
            builder.HasKey(s => s.Id);

            builder.Property(s => s.UserId)
                .IsRequired();

            builder.Property(s => s.ShopName)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(s => s.NationalCode)
                .HasMaxLength(10)
                .IsRequired();

            builder.Property(s => s.Status)
                .HasConversion<int>()
                .IsRequired();

            builder.Property(s => s.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

            builder.Property(s => s.UpdatedAt)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

            builder.HasMany(s => s.Inventories)
                .WithOne()
                .HasForeignKey(si => si.SellerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(s => s.UserId)
                .IsUnique()
                .HasDatabaseName("IX_Sellers_UserId");

            builder.HasIndex(s => s.NationalCode)
                .IsUnique()
                .HasDatabaseName("IX_Sellers_NationalCode");

            builder.HasIndex(s => s.ShopName)
                .HasDatabaseName("IX_Sellers_ShopName");

            builder.HasIndex(s => s.Status)
                .HasDatabaseName("IX_Sellers_Status");
        }
    }
}