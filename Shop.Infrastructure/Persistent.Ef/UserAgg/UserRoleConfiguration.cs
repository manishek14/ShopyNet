using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.UserAgg;

namespace Shop.Infrastructure.Persistent.Ef.UserAgg
{
    internal class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.ToTable("UserRoles", "User");
            builder.HasKey(ur => ur.Id);

            builder.Property(ur => ur.UserId).IsRequired();
            builder.Property(ur => ur.RoleId).IsRequired();

            builder.HasIndex(ur => ur.UserId)
                .HasDatabaseName("IX_UserRoles_UserId");

            builder.HasIndex(ur => ur.RoleId)
                .HasDatabaseName("IX_UserRoles_RoleId");

            builder.HasIndex(ur => new { ur.UserId, ur.RoleId })
                .IsUnique()
                .HasDatabaseName("IX_UserRoles_UserId_RoleId");
        }
    }
}