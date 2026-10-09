using Common.Aplication.FileUtil.Interfaces;
using Common.Aplication.FileUtil.Services;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Shop.Application._Utilities;
using Shop.Application.Audit;
using Microsoft.AspNetCore.Http;
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
using Shop.Query.Category.GetList;

namespace Shop.Config
{
    public static class ShopBootstrapper
    {
        public static void RegisterShopDependency(
            this IServiceCollection services,
            string connectionString)
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