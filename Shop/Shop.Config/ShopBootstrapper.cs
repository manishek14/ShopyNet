using Common.Aplication.FileUtil.Interfaces;
using Common.Aplication.FileUtil.Services;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Shop.Application._Utilities;
using Shop.Domain.CategoryAgg.Services;
using Shop.Domain.CommentAgg.Services;
using Shop.Domain.OrderAgg.Services;
using Shop.Domain.ProductAgg.Services;
using Shop.Domain.RoleAgg.Services;
using Shop.Domain.SellerAgg.Services;
using Shop.Domain.UserAgg.Service;
using Shop.Infrastructure;
using Shop.Infrastructure.CategoryAgg.Service;
using Shop.Infrastructure.CommentAgg.Service;
using Shop.Infrastructure.OrderAgg.Service;
using Shop.Infrastructure.ProductAgg.Service;
using Shop.Infrastructure.RoleAgg.Service;
using Shop.Infrastructure.SellerAgg.Service;
using Shop.Infrastructure.UserAgg.Service;
using Shop.Presentation.Facade;

namespace Shop.Config
{
    public static class ShopBootstrapper
    {
        public static void RegisterShopDependency(
            this IServiceCollection services,
            string connectionString)
        {
            // Infrastructure
            InfrastructureBootstrapper.Init(services, connectionString);

            // MediatR
            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssemblies(
                    typeof(Directories).Assembly,
                    typeof(Shop.Query.IBaseQuery<>).Assembly
                )
            );

            // FluentValidation
            services.AddValidatorsFromAssembly(typeof(Directories).Assembly);

            services.AddScoped<IFileService, FileService>();
            // Domain Serviceها
            RegisterDomainServices(services);
            // Initialize Facade Dependency
            services.InitFacadeDependency();
        }

        public static void RegisterDomainServices(IServiceCollection services)
        {
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