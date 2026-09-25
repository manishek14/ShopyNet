using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.Product.DTOs;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Query.Product.GetById
{
    internal class GetProductByIdQueryHandler : IBaseQueryHandler<GetProductByIdQuery, ProductDto>
    {
        private readonly ShopContext _shopContext;

        public GetProductByIdQueryHandler(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _shopContext.Products
                .Include(p => p.Images)
                .Include(p => p.Specifications)
                .SingleOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

            return product.Map();
        }
    }
}