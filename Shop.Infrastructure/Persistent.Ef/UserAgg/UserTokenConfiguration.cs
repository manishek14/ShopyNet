using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.UserAgg;

namespace Shop.Infrastructure.Persistent.Ef.UserAgg
{
    internal class UserTokenConfiguration : IEntityTypeConfiguration<UserToken>
    {
        public void Configure(EntityTypeBuilder<UserToken> builder)
        {
            builder.ToTable("UserTokens", "User");
            builder.HasKey(ut => ut.Id);

            builder.Property(ut => ut.UserId)
                .IsRequired();

            builder.Property(ut => ut.HashJwtToken)
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(ut => ut.HashRefreshToken)
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(ut => ut.TokenExpireDate)
                .IsRequired();

            builder.Property(ut => ut.RefreshTokenExpireDate)
                .IsRequired();

            builder.Property(ut => ut.Device)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(ut => ut.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

            builder.HasIndex(ut => ut.UserId)
                .HasDatabaseName("IX_UserTokens_UserId");

            builder.HasIndex(ut => ut.HashJwtToken)
                .HasDatabaseName("IX_UserTokens_HashJwtToken");

            builder.HasIndex(ut => ut.HashRefreshToken)
                .HasDatabaseName("IX_UserTokens_HashRefreshToken");
        }
    }
}