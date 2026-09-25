using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.Product.DTOs;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Query.Product.GetBySlug
{
    internal class GetProductBySlugQueryHandler
        : IBaseQueryHandler<GetProductBySlugQuery, ProductDto>
    {
        private readonly ShopContext _shopContext;

        public GetProductBySlugQueryHandler(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        public async Task<ProductDto> Handle(
            GetProductBySlugQuery request,
            CancellationToken cancellationToken)
        {
            var product = await _shopContext.Products
                .Include(p => p.Images)
                .Include(p => p.Specifications)
                .SingleOrDefaultAsync(p => p.Slug == request.Slug, cancellationToken);

            return product.Map();
        }
    }
}