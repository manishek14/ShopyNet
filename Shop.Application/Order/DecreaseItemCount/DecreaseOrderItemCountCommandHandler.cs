using Common.Aplication;
using Shop.Domain.OrderAgg.Enums;
using Shop.Domain.OrderAgg.Repositories;

namespace Shop.Application.Order.DecreaseItemCount
{
    public class DecreaseOrderItemCountCommandHandler
        : IBaseCommandHandler<DecreaseOrderItemCountCommand>
    {
        private readonly IOrderRepository _orderRepository;

        public DecreaseOrderItemCountCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository
                ?? throw new ArgumentNullException(nameof(orderRepository));
        }

        public async Task<OperationResult> Handle(
            DecreaseOrderItemCountCommand request,
            CancellationToken cancellationToken)
        {
            var order = (await _orderRepository.FindAsync(
                o => o.UserId == request.UserId && o.Status == OrderStatus.Pending,
                cancellationToken)).FirstOrDefault();

            if (order == null)
                return OperationResult.NotFound("Pending order not found.");

            var item = order.Items.FirstOrDefault(i => i.InventoryId == request.InventoryId);
            if (item == null)
                return OperationResult.NotFound("Item not found in order.");

            if (item.Count - request.Count <= 0)
            {
                order.RemoveItem(request.InventoryId);

                if (order.IsEmpty())
                {
                    _orderRepository.Remove(order);
                    await _orderRepository.SaveAsync(cancellationToken);
                    return OperationResult.Success();
                }
            }
            else
            {
                order.DecreaseItemCount(request.InventoryId, request.Count);
            }

            _orderRepository.Update(order);
            await _orderRepository.SaveAsync(cancellationToken);

            return OperationResult.Success();
        }
    }
}