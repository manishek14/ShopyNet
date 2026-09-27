using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shop.Domain.CategoryAgg;
using Shop.Domain.CategoryAgg.Services;
using Shop.Domain.CommentAgg;
using Shop.Domain.CommentAgg.Services;
using Shop.Domain.OrderAgg.Repositories;
using Shop.Domain.OrderAgg.Services;
using Shop.Domain.ProductAgg.Repository;
using Shop.Domain.ProductAgg.Services;
using Shop.Domain.RoleAgg;
using Shop.Domain.RoleAgg.Services;
using Shop.Domain.SellerAgg;
using Shop.Domain.SellerAgg.Services;
using Shop.Domain.UserAgg.Repository;  // ✅ اضافه کن
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
using Shop.Infrastructure.Persistent.Ef.UserAgg;  // ✅ اضافه کن
using Shop.Infrastructure.ProductAgg.Service;
using Shop.Infrastructure.RoleAgg.Service;
using Shop.Infrastructure.SellerAgg.Service;
using Shop.Infrastructure.UserAgg.Service;
using System;

namespace Shop.Infrastructure
{
    public static class InfrastructureBootstrapper
    {
        public static void Init(this IServiceCollection services, string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentNullException(nameof(connectionString));

            services.AddDbContext<ShopContext>(options =>
                options.UseSqlServer(connectionString));

            // ✅ Repositoryها
            services.AddScoped<IUserRepository, UserRepository>();  
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<ISellerRepository, SellerRepository>();

            // ✅ Domain Serviceها
            services.AddScoped<IDomainUserService, DomainUserService>();
            services.AddScoped<IProductDomainService, ProductDomainService>();
            services.AddScoped<IOrderDomainService, OrderDomainService>();
            services.AddScoped<ISellerDomainService, SellerDomainService>();
            services.AddScoped<IRoleDomainService, RoleDomainService>();
            services.AddScoped<ICategoryDomainService, CategoryDomainService>();
            services.AddScoped<ICommentDomainService, CommentDomainService>();

            // ✅ Password Hasher
            services.AddScoped<IPasswordHasher, PasswordHasher>();
        }
    }
}