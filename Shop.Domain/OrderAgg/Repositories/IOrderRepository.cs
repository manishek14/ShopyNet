using Clean_Arch.Query.Shared.Repository;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Domain.OrderAgg.Repositories
{
    public interface IOrderRepository : IBaseRepository<Order>
    {
        Task<Order> GetPendingOrderByUserIdAsync(
            Guid userId,
            CancellationToken cancellationToken = default);

        Task<Order> GetWithItemsAsync(
            Guid orderId,
            CancellationToken cancellationToken = default);

        Task<Order> GetLastOrderAsync(
            Guid userId,
            CancellationToken cancellationToken = default);
    }
}