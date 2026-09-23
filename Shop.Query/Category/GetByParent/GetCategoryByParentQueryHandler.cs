using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.Category.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Shop.Query.Category.GetByParent
{
    internal class GetCategoryByParentQueryHandler : IBaseQueryHandler<GetCategoryByParentQuery, List<CategoryWithParentDto>>
    {
        private readonly ShopContext _shopContext;

        public GetCategoryByParentQueryHandler(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        public async Task<List<CategoryWithParentDto>> Handle(GetCategoryByParentQuery request, CancellationToken cancellationToken)
        {
            var categories = await _shopContext.Categories
                .Where(c => c.ParentID == request.ParentId)
                .ToListAsync(cancellationToken);

            return CategoryMapper.SubParentMap(categories);
        }
    }
}
