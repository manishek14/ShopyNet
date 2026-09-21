using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.RoleAgg;

namespace Shop.Infrastructure.Persistent.Ef.RoleAgg
{
    internal class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Roles", "Role");
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Title)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(r => r.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

            builder.Property(r => r.UpdatedAt)
                .IsRequired(false);

            builder.HasMany(r => r.Permissions)
                .WithOne()
                .HasForeignKey(rp => rp.RoleId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(r => r.Title)
                .IsUnique()
                .HasDatabaseName("IX_Roles_Title");

            builder.HasIndex(r => r.CreatedAt)
                .HasDatabaseName("IX_Roles_CreatedAt");
        }
    }
}