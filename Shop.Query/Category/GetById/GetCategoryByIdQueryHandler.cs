using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.Category.DTOs;

namespace Shop.Query.Category.GetById
{
    internal class GetCategoryByIdQueryHandler : IBaseQueryHandler<GetCategoryByIdQuery, CategoryDto>
    {
        private readonly ShopContext _shopContext;

        public GetCategoryByIdQueryHandler(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        public async Task<CategoryDto> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            // Use SingleOrDefaultAsync to ensure the query uses EF Core async with cancellation support
            var id = request.Id;
            var category = await _shopContext.Categories
                .SingleOrDefaultAsync(c => c.Id == id, cancellationToken);

            return category.Map();
        }
    }
}
