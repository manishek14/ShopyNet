using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.OrderAgg;

namespace Shop.Infrastructure.Persistent.Ef.OrderAgg
{
    internal class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders", "Order");
            builder.HasKey(o => o.Id);

            builder.Property(o => o.UserId).IsRequired();
            builder.Property(o => o.Status).IsRequired().HasConversion<int>();
            builder.Property(o => o.CreatedAt).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(o => o.FinallyAt).IsRequired(false);

            // Discount (Value Object — Owned)
            builder.OwnsOne(o => o.Discount, discount =>
            {
                discount.Property(d => d.DiscountTitle)
                    .HasColumnName("DiscountTitle")
                    .HasMaxLength(200)
                    .IsRequired(false); 

                discount.Property(d => d.DiscountAmount)
                    .HasColumnName("DiscountAmount")
                    .IsRequired();     
            });

            // Address (Entity — HasOne)
            builder.HasOne(o => o.Address)
                .WithOne(a => a.Order)
                .HasForeignKey<OrderAddress>(a => a.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            // ShippingMethod (Value Object — Owned)
            builder.OwnsOne(o => o.ShippingMethod, shipping =>
            {
                shipping.Property(s => s.ShippingType)
                    .HasColumnName("ShippingType")
                    .HasMaxLength(100)
                    .IsRequired(false);

                shipping.Property(s => s.ShippingCost)
                    .HasColumnName("ShippingCost")
                    .IsRequired();  
            });

            // Indexes
            builder.HasIndex(o => o.UserId).HasDatabaseName("IX_Orders_UserId");
            builder.HasIndex(o => o.Status).HasDatabaseName("IX_Orders_Status");
            builder.HasIndex(o => o.CreatedAt).HasDatabaseName("IX_Orders_CreatedAt");
        }
    }
}