using Microsoft.EntityFrameworkCore;
using Shop.Domain.CategoryAgg;
using Shop.Domain.CommentAgg;
using Shop.Domain.OrderAgg;
using Shop.Domain.ProductAgg;
using Shop.Domain.RoleAgg;
using Shop.Domain.SellerAgg;
using Shop.Domain.UserAgg;

// ✅ Configurationها
using Shop.Infrastructure.Persistent.Ef.CategoryAgg;
using Shop.Infrastructure.Persistent.Ef.CommentAgg;
using Shop.Infrastructure.Persistent.Ef.OrderAgg;
using Shop.Infrastructure.Persistent.Ef.ProductAgg;
using Shop.Infrastructure.Persistent.Ef.RoleAgg;
using Shop.Infrastructure.Persistent.Ef.SellerAgg;
using Shop.Infrastructure.Persistent.Ef.UserAgg;

namespace Shop.Infrastructure.Persistent.Ef
{
    public class ShopContext : DbContext
    {
        public ShopContext() { }
        public ShopContext(DbContextOptions<ShopContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    "Data Source=DESKTOP-6NC34ET;Database=ShopyNet;Integrated Security=True;TrustServerCertificate=True;MultipleActiveResultSets=true;"
                );
            }
        }

        // DbSets
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ProductSpecification> ProductSpecifications { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Seller> Sellers { get; set; }
        public DbSet<SellerInventory> SellerInventories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<UserAddress> UserAddresses { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<UserToken> UserTokens { get; set; }

        // Enums configuration
        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<Enum>().HaveConversion<int>();
        }

        // Configuration
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Gender 
            modelBuilder.Entity<User>()
                .Property(u => u.Gender)
                .HasConversion<int>();

            // UserAgg
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
            modelBuilder.ApplyConfiguration(new WalletConfiguration());
            modelBuilder.ApplyConfiguration(new UserAddressConfiguration());
            modelBuilder.ApplyConfiguration(new UserTokenConfiguration());

            // ProductAgg
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new ProductImageConfiguration());
            modelBuilder.ApplyConfiguration(new ProductSpecificationConfiguration());

            // OrderAgg
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new OrderItemConfiguration());

            // CommentAgg
            modelBuilder.ApplyConfiguration(new CommentConfiguration());

            // CategoryAgg
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());

            // RoleAgg
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new RolePermissionConfiguration());

            // SellerAgg
            modelBuilder.ApplyConfiguration(new SellerConfiguration());
            modelBuilder.ApplyConfiguration(new SellerInventoryConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}