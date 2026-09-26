using Microsoft.EntityFrameworkCore;
using Shop.Domain.OrderAgg;
using Shop.Domain.OrderAgg.Repositories;
using Shop.Infrastructure._Utilities;
using Shop.Infrastructure.Persistent.Ef;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Infrastructure.Persistent.Ef.OrderAgg
{
    internal class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(ShopContext context) : base(context)
        {
        } 
        public async Task<Order> GetPendingOrderByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .AsNoTracking()
                .Include(o => o.Items)
                .FirstOrDefaultAsync(o => o.UserId == userId && o.Status == Shop.Domain.OrderAgg.Enums.OrderStatus.Pending, cancellationToken);
        }

        public async Task<Order> GetWithItemsAsync(Guid orderId, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .Include(o => o.Items)
                .FirstOrDefaultAsync(o => o.Id == orderId, cancellationToken);
        }

        public async Task<Order> GetLastOrderAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .Where(o => o.UserId == userId)
                .OrderByDescending(o => o.CreatedAt)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}