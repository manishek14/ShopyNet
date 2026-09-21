using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.OrderAgg;
using Shop.Domain.OrderAgg.ValueObject;
using System;

namespace Shop.Infrastructure.Persistent.Ef.OrderAgg
{
    internal class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders", "Order");
            builder.HasKey(o => o.Id);

            builder.Property(o => o.UserId)
                .IsRequired();

            builder.Property(o => o.Status)
                .IsRequired()
                .HasConversion<int>();  

            builder.Property(o => o.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

            builder.Property(o => o.FinallyAt)
                .IsRequired(false);

            builder.HasMany(o => o.Items)
                .WithOne()
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.OwnsOne(o => o.Discount, discount =>
            {
                discount.Property(d => d.DiscountTitle)
                    .HasColumnName("DiscountTitle")
                    .HasMaxLength(200)
                    .IsRequired(false);

                discount.Property(d => d.DiscountAmount)
                    .HasColumnName("DiscountAmount")
                    .IsRequired(false);
            });

            builder.OwnsOne(o => o.Address, address =>
            {
                address.Property(a => a.Province)
                    .HasColumnName("Province")
                    .HasMaxLength(100)
                    .IsRequired(false);

                address.Property(a => a.City)
                    .HasColumnName("City")
                    .HasMaxLength(100)
                    .IsRequired(false);

                address.Property(a => a.PostalCode)
                    .HasColumnName("PostalCode")
                    .HasMaxLength(10)
                    .IsRequired(false);

                address.Property(a => a.MailingAddress)
                    .HasColumnName("MailingAddress")
                    .HasMaxLength(500)
                    .IsRequired(false);

                address.Property(a => a.PhoneNumber)
                    .HasColumnName("PhoneNumber")
                    .HasMaxLength(11)
                    .IsRequired(false);

                address.Property(a => a.Name)
                    .HasColumnName("Name")
                    .HasMaxLength(100)
                    .IsRequired(false);

                address.Property(a => a.Family)
                    .HasColumnName("Family")
                    .HasMaxLength(100)
                    .IsRequired(false);

                address.Property(a => a.NationalCode)
                    .HasColumnName("NationalCode")
                    .HasMaxLength(10)
                    .IsRequired(false);
            });

            builder.OwnsOne(o => o.ShippingMethod, shipping =>
            {
                shipping.Property(s => s.ShippingType)
                    .HasColumnName("ShippingType")
                    .HasMaxLength(100)
                    .IsRequired(false);

                shipping.Property(s => s.ShippingCost)
                    .HasColumnName("ShippingCost")
                    .IsRequired(false);
            });

            builder.HasIndex(o => o.UserId)
                .HasDatabaseName("IX_Orders_UserId");

            builder.HasIndex(o => o.Status)
                .HasDatabaseName("IX_Orders_Status");

            builder.HasIndex(o => o.CreatedAt)
                .HasDatabaseName("IX_Orders_CreatedAt");
        }
    }
}