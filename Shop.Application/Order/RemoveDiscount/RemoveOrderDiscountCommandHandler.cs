using Common.Aplication;
using Shop.Domain.OrderAgg.Enums;
using Shop.Domain.OrderAgg.Repositories;

namespace Shop.Application.Order.RemoveDiscount
{
    public class RemoveOrderDiscountCommandHandler
        : IBaseCommandHandler<RemoveOrderDiscountCommand>
    {
        private readonly IOrderRepository _orderRepository;

        public RemoveOrderDiscountCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository
                ?? throw new ArgumentNullException(nameof(orderRepository));
        }

        public async Task<OperationResult> Handle(
            RemoveOrderDiscountCommand request,
            CancellationToken cancellationToken)
        {
            var order = (await _orderRepository.FindAsync(
                o => o.UserId == request.UserId && o.Status == OrderStatus.Pending,
                cancellationToken)).FirstOrDefault();

            if (order == null)
                return OperationResult.NotFound("Pending order not found.");

            order.RemoveDiscount();

            _orderRepository.Update(order);
            await _orderRepository.SaveAsync(cancellationToken);

            return OperationResult.Success();
        }
    }
}