using Common.Aplication.FileUtil.Interfaces;
using Common.Aplication.FileUtil.Services;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shop.Application._Utilities;
using Shop.Application.Audit;
using Shop.Application.Caching;
using Common.Application.Email;
using Shop.Domain.CategoryAgg.Services;
using Shop.Domain.CommentAgg.Services;
using Shop.Domain.OrderAgg.Services;
using Shop.Domain.ProductAgg.Services;
using Shop.Domain.RoleAgg.Services;
using Shop.Domain.SellerAgg.Services;
using Shop.Domain.UserAgg.Service;
using Shop.Infrastructure;
using Shop.Infrastructure.Caching;
using Shop.Infrastructure.CategoryAgg.Service;
using Shop.Infrastructure.CommentAgg.Service;
using Shop.Infrastructure.Email;
using Shop.Infrastructure.OrderAgg.Service;
using Shop.Infrastructure.ProductAgg.Service;
using Shop.Infrastructure.RoleAgg.Service;
using Shop.Infrastructure.SellerAgg.Service;
using Shop.Infrastructure.UserAgg.Service;
using Shop.Query.Category.GetList;
using StackExchange.Redis;

namespace Shop.Config
{
    public static class ShopBootstrapper
    {
        public static void RegisterShopDependency(
            this IServiceCollection services,
            string connectionString,
            IConfiguration configuration)
        {
            InfrastructureBootstrapper.Init(services, connectionString);

            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssemblies(
                    typeof(Directories).Assembly,
                    typeof(GetCategoriesListQueryHandler).Assembly
                )
            );

            services.AddValidatorsFromAssembly(typeof(Directories).Assembly);

            services.AddScoped<IFileService, FileService>(); 
            services.AddScoped<IDirectories, DirectoriesService>();

            services.AddHttpContextAccessor();

            RegisterDomainServices(services);

            // Redis & Cache registration
            var redisSettings = configuration.GetSection("Redis").Get<RedisSettings>()
                ?? new RedisSettings { ConnectionString = "localhost:6379" };

            services.AddSingleton<IConnectionMultiplexer>(sp =>
                ConnectionMultiplexer.Connect(redisSettings.ConnectionString));

            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = redisSettings.ConnectionString;
                options.InstanceName = redisSettings.InstanceName;
            });

            services.Configure<RedisSettings>(configuration.GetSection("Redis"));
            services.AddScoped<ICacheService, RedisCacheService>();

            services.Configure<EmailSettings>(configuration.GetSection("Email"));
            services.AddScoped<IEmailService, EmailService>();
        }

        private static void RegisterDomainServices(IServiceCollection services)
        {
            services.AddScoped<IAuditService, AuditService>();
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