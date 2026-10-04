using Common.Aplication;
using Shop.Domain.OrderAgg;
using Shop.Domain.OrderAgg.Enums;
using Shop.Domain.OrderAgg.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Application.Order.AddItem
{
    public class AddOrderItemCommandHandler : IBaseCommandHandler<AddOrderItemCommand>
    {
        private readonly IOrderRepository _orderRepository;

        public AddOrderItemCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<OperationResult> Handle(
            AddOrderItemCommand request,
            CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetPendingOrderByUserIdAsync(
                request.UserId, cancellationToken);

            var item = new OrderItem(
                request.InventoryId,
                request.Count,
                request.Price
            );

            if (order == null)
            {
                // Create order and add item before saving to avoid concurrency/race issues
                order = new Domain.OrderAgg.Order(request.UserId);
                order.AddItem(item);
                await _orderRepository.AddAsync(order, cancellationToken);

                await _orderRepository.SaveAsync(cancellationToken);
            }
            else
            {
                order.AddItem(item);
                await _orderRepository.SaveAsync(cancellationToken);
            }

            return OperationResult.Success();
        }
    }
}