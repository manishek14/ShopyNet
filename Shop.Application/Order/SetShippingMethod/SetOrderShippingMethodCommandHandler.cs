using Common.Aplication;
using Shop.Domain.OrderAgg.Enums;
using Shop.Domain.OrderAgg.Repositories;
using Shop.Domain.OrderAgg.ValueObject;

namespace Shop.Application.Order.SetShippingMethod
{
    public class SetOrderShippingMethodCommandHandler
        : IBaseCommandHandler<SetOrderShippingMethodCommand>
    {
        private readonly IOrderRepository _orderRepository;

        public SetOrderShippingMethodCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository
                ?? throw new ArgumentNullException(nameof(orderRepository));
        }

        public async Task<OperationResult> Handle(
            SetOrderShippingMethodCommand request,
            CancellationToken cancellationToken)
        {
            var order = (await _orderRepository.FindAsync(
                o => o.UserId == request.UserId && o.Status == OrderStatus.Pending,
                cancellationToken)).FirstOrDefault();

            if (order == null)
                return OperationResult.NotFound("Pending order not found.");

            var shippingMethod = new ShippingMethod(
                request.ShippingType,
                request.ShippingCost
            );

            order.SetShippingMethod(shippingMethod);

            _orderRepository.Update(order);
            await _orderRepository.SaveAsync(cancellationToken);

            return OperationResult.Success();
        }
    }
}