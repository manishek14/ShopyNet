using Shop.Domain.OrderAgg;
using Shop.Domain.ProductAgg;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Domain.OrderAgg.Services
{
    public interface IOrderDomainService
    {
        bool IsProductExist(Guid productId);

        Task<bool> IsProductExistAsync(Guid productId, CancellationToken cancellationToken = default);

        bool IsProductInStock(Guid productId, int requestedCount);
        Task<bool> IsProductInStockAsync(Guid productId, int requestedCount, CancellationToken cancellationToken = default);

        int CalculateTotalPrice(IEnumerable<OrderItem> items);

        bool IsOrderItemLimitExceeded(IEnumerable<OrderItem> items);

        bool IsOrderLimitExceeded(Guid userId);

        Task<bool> IsOrderLimitExceededAsync(Guid userId, CancellationToken cancellationToken = default);
    }
}
