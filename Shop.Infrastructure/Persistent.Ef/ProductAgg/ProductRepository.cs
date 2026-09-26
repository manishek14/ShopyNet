using Microsoft.EntityFrameworkCore;
using Shop.Domain.ProductAgg;
using Shop.Domain.ProductAgg.Repository;
using Shop.Infrastructure._Utilities;
using Shop.Infrastructure.Persistent.Ef;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Infrastructure.Persistent.Ef.ProductAgg
{
    internal class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(ShopContext context) : base(context)
        {
        }

        public async Task<Product> GetBySlugAsync(string slug, CancellationToken cancellationToken = default)
        {
            return await _dbSet.FirstOrDefaultAsync(p => p.Slug == slug, cancellationToken);
        }

        public async Task<Product> GetWithDetailsAsync(Guid productId, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .Include(p => p.Images)
                .Include(p => p.Specifications)
                .FirstOrDefaultAsync(p => p.Id == productId, cancellationToken);
        }

        public async Task<bool> IsSlugExistAsync(string slug, CancellationToken cancellationToken = default)
        {
            return await _dbSet.AnyAsync(p => p.Slug == slug, cancellationToken);
        }
    }
}
