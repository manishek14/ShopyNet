using Microsoft.Extensions.DependencyInjection;
using Shop.Presentation.Facade.Category;
using Shop.Presentation.Facade.Comment;
using Shop.Presentation.Facade.Order;
using Shop.Presentation.Facade.Product;
using Shop.Presentation.Facade.Role;
using Shop.Presentation.Facade.Seller;
using Shop.Presentation.Facade.User;

namespace Shop.Presentation.Facade
{
    public static class FacadeBootstrapper
    {
        public static IServiceCollection InitFacadeDependency(this IServiceCollection services)
        {
            services.AddScoped<ICategoryFacade, CategoryFacade>();
            services.AddScoped<ICommentFacade, CommentFacade>();
            services.AddScoped<IOrderFacade, OrderFacade>();
            services.AddScoped<IProductFacade, ProductFacade>();
            services.AddScoped<IRoleFacade, RoleFacade>();
            services.AddScoped<ISellerFacade, SellerFacade>();
            services.AddScoped<IUserFacade, UserFacade>();

            return services;
        }
    }
}