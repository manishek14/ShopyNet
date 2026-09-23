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
            var category = await _shopContext.Categories.FindAsync(request.Id);
            return category.Map();
        }
    }
}
