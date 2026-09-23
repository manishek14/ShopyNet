using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.Category.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shop.Query.Category;

namespace Shop.Query.Category.GetList
{
    internal class GetCategoriesListQueryHandler : IBaseQueryHandler<GetCategoriesListQuery, List<CategoryWithChildsDto>>
    {
        private readonly ShopContext _shopContext;

        public GetCategoriesListQueryHandler(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        public async Task<List<CategoryWithChildsDto>> Handle(GetCategoriesListQuery request, CancellationToken cancellationToken)
        {
            var categories = await _shopContext.Categories
                .Where(c => c.ParentID == null)
                .ToListAsync(cancellationToken);

            return CategoryMapper.SubMap(categories);
        }
    }
}
