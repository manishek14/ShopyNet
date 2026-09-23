using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.Category.DTOs;

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
            var parentId = request.ParentId;
            var categories = await _shopContext.Categories
                .Where(c => c.ParentID == parentId)
                .ToListAsync(cancellationToken);

            return CategoryMapper.SubParentMap(categories);
        }
    }
}
