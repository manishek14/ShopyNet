using Common.Aplication;
using Shop.Domain.OrderAgg.Enums;
using Shop.Domain.OrderAgg.Repositories;
using Shop.Domain.OrderAgg.ValueObject;

namespace Shop.Application.Order.ApplyDiscount
{
    public class ApplyOrderDiscountCommandHandler
        : IBaseCommandHandler<ApplyOrderDiscountCommand>
    {
        private readonly IOrderRepository _orderRepository;

        public ApplyOrderDiscountCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository
                ?? throw new ArgumentNullException(nameof(orderRepository));
        }

        public async Task<OperationResult> Handle(
            ApplyOrderDiscountCommand request,
            CancellationToken cancellationToken)
        {
            var order = (await _orderRepository.FindAsync(
                o => o.UserId == request.UserId && o.Status == OrderStatus.Pending,
                cancellationToken)).FirstOrDefault();

            if (order == null)
                return OperationResult.NotFound("Pending order not found.");

            var discount = new OrderDiscount(
                request.DiscountTitle,
                request.DiscountAmount
            );

            order.ApplyDiscount(discount);

            _orderRepository.Update(order);
            await _orderRepository.SaveAsync(cancellationToken);

            return OperationResult.Success();
        }
    }
}