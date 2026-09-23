using Microsoft.EntityFrameworkCore;
using Shop.Domain.CategoryAgg;

namespace Shop.Infrastructure.Persistent.Ef
{
    public class ShopContext : DbContext
    {
        public ShopContext(DbContextOptions<ShopContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Basic configuration placeholder. Specific configurations should live in separate configuration classes.
            base.OnModelCreating(modelBuilder);
        }
    }
}
