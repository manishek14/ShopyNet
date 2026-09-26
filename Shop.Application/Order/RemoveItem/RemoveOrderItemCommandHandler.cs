using Common.Aplication;
using Shop.Domain.OrderAgg.Enums;
using Shop.Domain.OrderAgg.Repositories;

namespace Shop.Application.Order.RemoveItem
{
    public class RemoveOrderItemCommandHandler : IBaseCommandHandler<RemoveOrderItemCommand>
    {
        private readonly IOrderRepository _orderRepository;

        public RemoveOrderItemCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository
                ?? throw new ArgumentNullException(nameof(orderRepository));
        }

        public async Task<OperationResult> Handle(
            RemoveOrderItemCommand request,
            CancellationToken cancellationToken)
        {
            var order = (await _orderRepository.FindAsync(
                o => o.UserId == request.UserId && o.Status == OrderStatus.Pending,
                cancellationToken)).FirstOrDefault();

            if (order == null)
                return OperationResult.NotFound("Pending order not found.");

            order.RemoveItem(request.InventoryId);

            if (order.IsEmpty())
            {
                _orderRepository.Remove(order);
                await _orderRepository.SaveAsync(cancellationToken);
                return OperationResult.Success();
            }

            _orderRepository.Update(order);
            await _orderRepository.SaveAsync(cancellationToken);

            return OperationResult.Success();
        }
    }
}