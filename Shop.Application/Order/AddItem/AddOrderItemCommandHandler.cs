using Common.Aplication;
using Shop.Domain.OrderAgg;
using Shop.Domain.OrderAgg.Enums;
using Shop.Domain.OrderAgg.Repositories;

namespace Shop.Application.Order.AddItem
{
    public class AddOrderItemCommandHandler : IBaseCommandHandler<AddOrderItemCommand>
    {
        private readonly IOrderRepository _orderRepository;

        public AddOrderItemCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository
                ?? throw new ArgumentNullException(nameof(orderRepository));
        }

        public async Task<OperationResult> Handle(
            AddOrderItemCommand request,
            CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.FindAsync(
                o => o.UserId == request.UserId && o.Status == OrderStatus.Pending,
                cancellationToken);

            var order = orders.FirstOrDefault();

            if (order == null)
            {
                order = new Domain.OrderAgg.Order(request.UserId);
                await _orderRepository.AddAsync(order, cancellationToken);
            }

            var item = new OrderItem(
                request.InventoryId,
                request.Count,
                request.Price
            );

            order.AddItem(item);

            _orderRepository.Update(order);
            await _orderRepository.SaveAsync(cancellationToken);

            return OperationResult.Success();
        }
    }
}