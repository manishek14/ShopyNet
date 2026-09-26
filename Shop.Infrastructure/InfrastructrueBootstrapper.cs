using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shop.Domain.CategoryAgg;
using Shop.Domain.CategoryAgg.Services;
using Shop.Domain.CommentAgg;
using Shop.Domain.CommentAgg.Services;
using Shop.Domain.OrderAgg;
using Shop.Domain.OrderAgg.Repositories;
using Shop.Domain.OrderAgg.Services;
using Shop.Domain.ProductAgg;
using Shop.Domain.ProductAgg.Services;
using Shop.Domain.RoleAgg;
using Shop.Domain.RoleAgg.Services;
using Shop.Domain.SellerAgg;
using Shop.Domain.SellerAgg.Services;
using Shop.Domain.UserAgg;
using Shop.Domain.UserAgg.Service;
using Shop.Infrastructure.CategoryAgg.Service;
using Shop.Infrastructure.CommentAgg.Service;
using Shop.Infrastructure.OrderAgg.Service;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Infrastructure.Persistent.Ef.CategoryAgg;
using Shop.Infrastructure.Persistent.Ef.CommentAgg;
using Shop.Infrastructure.Persistent.Ef.OrderAgg;
using Shop.Infrastructure.Persistent.Ef.ProductAgg;
using Shop.Infrastructure.Persistent.Ef.RoleAgg;
using Shop.Infrastructure.Persistent.Ef.SellerAgg;
using Shop.Infrastructure.Persistent.Ef.UserAgg;
using Shop.Infrastructure.ProductAgg.Service;
using Shop.Infrastructure.RoleAgg.Service;
using Shop.Infrastructure.SellerAgg.Service;
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

            // Register repositories as Transient
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<ICommentRepository, CommentRepository>();
            services.AddTransient<IRoleRepository, RoleRepository>();
            services.AddTransient<ISellerRepository, SellerRepository>();

            // Domain Service
            services.AddScoped<IDomainUserService, DomainUserService>();
            services.AddScoped<IProductDomainService, ProductDomainService>();
            services.AddScoped<IOrderDomainService, OrderDomainService>();
            services.AddScoped<ISellerDomainService, SellerDomainService>();
            services.AddScoped<IRoleDomainService, RoleDomainService>();
            services.AddScoped<ICategoryDomainService, CategoryDomainService>();
            services.AddScoped<ICommentDomainService, CommentDomainService>();
        }
    }
}