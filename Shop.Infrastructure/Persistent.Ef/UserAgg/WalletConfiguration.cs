using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.UserAgg;

namespace Shop.Infrastructure.Persistent.Ef.UserAgg
{
    internal class WalletConfiguration : IEntityTypeConfiguration<Wallet>
    {
        public void Configure(EntityTypeBuilder<Wallet> builder)
        {
            builder.ToTable("Wallets", "User");
            builder.HasKey(w => w.Id);

            builder.Property(w => w.UserId)
                .IsRequired();

            builder.Property(w => w.Price)
                .IsRequired();

            builder.Property(w => w.Description)
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(w => w.Type)
                .HasConversion<int>()
                .IsRequired();

            builder.Property(w => w.IsFinally)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(w => w.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

            builder.Property(w => w.FinallyAt)
                .IsRequired(false);

            builder.HasIndex(w => w.UserId)
                .HasDatabaseName("IX_Wallets_UserId");

            builder.HasIndex(w => w.Type)
                .HasDatabaseName("IX_Wallets_Type");

            builder.HasIndex(w => w.IsFinally)
                .HasDatabaseName("IX_Wallets_IsFinally");

            builder.HasIndex(w => w.CreatedAt)
                .HasDatabaseName("IX_Wallets_CreatedAt");

            builder.HasIndex(w => new { w.UserId, w.Type })
                .HasDatabaseName("IX_Wallets_UserId_Type");

            builder.HasIndex(w => new { w.UserId, w.IsFinally })
                .HasDatabaseName("IX_Wallets_UserId_IsFinally");
        }
    }
}