using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.UserAgg;

namespace Shop.Infrastructure.Persistent.Ef.UserAgg
{
    internal class UserAddressConfiguration : IEntityTypeConfiguration<UserAddress>
    {
        public void Configure(EntityTypeBuilder<UserAddress> builder)
        {
            builder.ToTable("UserAddresses", "User");
            builder.HasKey(ua => ua.Id);

            builder.Property(ua => ua.UserId).IsRequired();

            builder.Property(ua => ua.Province)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(ua => ua.City)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(ua => ua.PostalCode)
                .HasMaxLength(10)
                .IsRequired();

            builder.Property(ua => ua.MailingAddress)
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(ua => ua.PhoneNumber)
                .HasMaxLength(11)
                .IsRequired();

            builder.Property(ua => ua.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(ua => ua.Family)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(ua => ua.NationalCode)
                .HasMaxLength(10)
                .IsRequired();

            builder.Property(ua => ua.IsActive)
                .IsRequired()
                .HasDefaultValue(true);

            builder.Property(ua => ua.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

            builder.Property(ua => ua.UpdatedAt)
                .IsRequired(false);

            builder.HasIndex(ua => ua.UserId)
                .HasDatabaseName("IX_UserAddresses_UserId");

            builder.HasIndex(ua => ua.IsActive)
                .HasDatabaseName("IX_UserAddresses_IsActive");
        }
    }
}