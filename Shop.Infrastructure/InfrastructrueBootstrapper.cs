using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shop.Infrastructure.Persistent.Dapper;
using Shop.Infrastructure.Persistent.Ef;
using System;

namespace Shop.Infrastructure
{
    public static class InfrastructureBootstrapper
    {
        public static void Init(this IServiceCollection services, string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentNullException(nameof(connectionString));
            services.AddDbContext<ShopContext>(
                options => options.UseSqlServer(connectionString),
                contextLifetime: ServiceLifetime.Transient,
                optionsLifetime: ServiceLifetime.Transient);

            // Register Dapper context and DbContext
            services.AddTransient<DapperContext>(provider => new DapperContext(connectionString));
            services.AddTransient<ShopContext>(options =>
            {
                var dbContextOptions = new DbContextOptionsBuilder<ShopContext>()
                    .UseSqlServer(connectionString)
                    .Options;
                return new ShopContext(dbContextOptions);
            });
        }
    }
}