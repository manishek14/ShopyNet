using Shop.Domain.OrderAgg;
using Shop.Domain.OrderAgg.Enums;
using Shop.Domain.OrderAgg.Repositories;
using Shop.Domain.OrderAgg.Services;
using Shop.Domain.ProductAgg.Repository;
using Shop.Domain.ProductAgg;
using Shop.Domain.ProductAgg.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Infrastructure.OrderAgg.Service
{
    public class OrderDomainService : IOrderDomainService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;

        public OrderDomainService(
            IOrderRepository orderRepository,
            IProductRepository productRepository)
        {
            _orderRepository = orderRepository
                ?? throw new ArgumentNullException(nameof(orderRepository));
            _productRepository = productRepository
                ?? throw new ArgumentNullException(nameof(productRepository));
        }

        public bool IsProductExist(Guid productId)
        {
            if (productId == Guid.Empty)
                return false;

            return _productRepository.Exists(p => p.Id == productId && p.IsActive);
        }

        public async Task<bool> IsProductExistAsync(
            Guid productId,
            CancellationToken cancellationToken = default)
        {
            if (productId == Guid.Empty)
                return false;

            return await _productRepository.ExistsAsync(
                p => p.Id == productId && p.IsActive,
                cancellationToken);
        }

        public bool IsProductInStock(Guid productId, int requestedCount)
        {
            if (productId == Guid.Empty || requestedCount <= 0)
                return false;

            return true;
        }

        public async Task<bool> IsProductInStockAsync(
            Guid productId,
            int requestedCount,
            CancellationToken cancellationToken = default)
        {
            return await Task.FromResult(IsProductInStock(productId, requestedCount));
        }

        public int CalculateTotalPrice(IEnumerable<OrderItem> items)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));

            return items.Sum(item => item.TotalPrice);
        }

        public bool IsOrderItemLimitExceeded(IEnumerable<OrderItem> items)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));

            const int maxItems = 50;
            return items.Count() > maxItems;
        }

        public bool IsOrderLimitExceeded(Guid userId)
        {
            if (userId == Guid.Empty)
                return false;

            const int maxActiveOrders = 10;
            var activeOrders = _orderRepository
                .FindAsync(o => o.UserId == userId && o.Status == OrderStatus.Pending)
                .Result;

            return activeOrders.Count() >= maxActiveOrders;
        }

        public async Task<bool> IsOrderLimitExceededAsync(
            Guid userId,
            CancellationToken cancellationToken = default)
        {
            if (userId == Guid.Empty)
                return false;

            const int maxActiveOrders = 10;
            var activeOrders = await _orderRepository.FindAsync(
                o => o.UserId == userId && o.Status == OrderStatus.Pending,
                cancellationToken);

            return activeOrders.Count >= maxActiveOrders;
        }
    }
}