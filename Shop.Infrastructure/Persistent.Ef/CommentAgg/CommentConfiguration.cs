using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.CommentAgg;

namespace Shop.Infrastructure.Persistent.Ef.CommentAgg
{
    internal class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("Comments", "Comment");
            builder.HasKey(c => c.Id);

            builder.Property(c => c.UserId)
                .IsRequired();

            builder.Property(c => c.ProductId)
                .IsRequired();

            builder.Property(c => c.ReplyId)
                .IsRequired(false);

            builder.Property(c => c.content)
                .HasColumnName("Content")
                .HasMaxLength(1000)
                .IsRequired();

            builder.Property(c => c.status)
                .HasColumnName("Status")
                .HasConversion<int>()
                .IsRequired();

            builder.Property(c => c.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

            builder.Property(c => c.UpdatedAt)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

            builder.HasIndex(c => c.UserId)
                .HasDatabaseName("IX_Comments_UserId");

            builder.HasIndex(c => c.ProductId)
                .HasDatabaseName("IX_Comments_ProductId");

            builder.HasIndex(c => c.status)
                .HasDatabaseName("IX_Comments_Status");

            builder.HasIndex(c => c.CreatedAt)
                .HasDatabaseName("IX_Comments_CreatedAt");

            builder.HasIndex(c => new { c.ProductId, c.status })
                .HasDatabaseName("IX_Comments_ProductId_Status");
        }
    }
}