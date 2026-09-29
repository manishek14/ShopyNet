using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.RoleAgg;

namespace Shop.Infrastructure.Persistent.Ef.RoleAgg
{
    internal class RolePermissionConfiguration : IEntityTypeConfiguration<Role.RolePermission>
    {
        public void Configure(EntityTypeBuilder<Role.RolePermission> builder)
        {
            builder.ToTable("RolePermissions", "Role");
            builder.HasKey(rp => rp.Id);

            builder.Property(rp => rp.RoleId).IsRequired();

            builder.Property(rp => rp.Permission)
                .HasConversion<int>()
                .IsRequired();

            builder.HasIndex(rp => rp.RoleId)
                .HasDatabaseName("IX_RolePermissions_RoleId");

            builder.HasIndex(rp => new { rp.RoleId, rp.Permission })
                .IsUnique()
                .HasDatabaseName("IX_RolePermissions_RoleId_Permission");
        }
    }
}