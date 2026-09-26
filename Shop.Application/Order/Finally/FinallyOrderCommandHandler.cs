using Common.Aplication;
using Shop.Domain.OrderAgg.Enums;
using Shop.Domain.OrderAgg.Repositories;

namespace Shop.Application.Order.Finally
{
    public class FinallyOrderCommandHandler
        : IBaseCommandHandler<FinallyOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;

        public FinallyOrderCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository
                ?? throw new ArgumentNullException(nameof(orderRepository));
        }

        public async Task<OperationResult> Handle(
            FinallyOrderCommand request,
            CancellationToken cancellationToken)
        {
            var order = (await _orderRepository.FindAsync(
                o => o.UserId == request.UserId && o.Status == OrderStatus.Finally,
                cancellationToken)).FirstOrDefault();

            if (order == null)
                return OperationResult.NotFound("Finally order not found.");

            order.Finally();

            _orderRepository.Update(order);
            await _orderRepository.SaveAsync(cancellationToken);

            return OperationResult.Success();
        }
    }
}