using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.AuditAgg;

namespace Shop.Infrastructure.Persistent.Ef.AuditAgg
{
    internal class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
    {
        public void Configure(EntityTypeBuilder<AuditLog> builder)
        {
            builder.ToTable("AuditLogs", "Audit");
            builder.HasKey(a => a.Id);

            builder.Property(a => a.UserId).IsRequired(false);
            builder.Property(a => a.Action).HasMaxLength(100).IsRequired();
            builder.Property(a => a.EntityName).HasMaxLength(200).IsRequired();
            builder.Property(a => a.EntityId).HasMaxLength(100).IsRequired();
            builder.Property(a => a.OldValues).IsRequired(false);
            builder.Property(a => a.NewValues).IsRequired(false);
            builder.Property(a => a.IpAddress).HasMaxLength(50).IsRequired(false);
            builder.Property(a => a.UserAgent).HasMaxLength(500).IsRequired(false);
            builder.Property(a => a.CreatedAt).IsRequired().HasDefaultValueSql("GETDATE()");

            builder.HasIndex(a => a.UserId).HasDatabaseName("IX_AuditLogs_UserId");
            builder.HasIndex(a => a.EntityName).HasDatabaseName("IX_AuditLogs_EntityName");
            builder.HasIndex(a => a.CreatedAt).HasDatabaseName("IX_AuditLogs_CreatedAt");
        }
    }
}