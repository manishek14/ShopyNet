using Common.Aplication;
using Shop.Domain.OrderAgg;
using Shop.Domain.OrderAgg.Enums;
using Shop.Domain.OrderAgg.Repositories;

namespace Shop.Application.Order.SetAddress
{
    public class SetOrderAddressCommandHandler
        : IBaseCommandHandler<SetOrderAddressCommand>
    {
        private readonly IOrderRepository _orderRepository;

        public SetOrderAddressCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository
                ?? throw new ArgumentNullException(nameof(orderRepository));
        }

        public async Task<OperationResult> Handle(
            SetOrderAddressCommand request,
            CancellationToken cancellationToken)
        {
            var order = (await _orderRepository.FindAsync(
                o => o.UserId == request.UserId && o.Status == OrderStatus.Pending,
                cancellationToken)).FirstOrDefault();

            if (order == null)
                return OperationResult.NotFound("Pending order not found.");

            // ✅ ایجاد آدرس
            var address = new OrderAddress(
                request.Province,
                request.City,
                request.PostalCode,
                request.MailingAddress,
                request.PhoneNumber,
                request.Name,
                request.Family,
                request.NationalCode,
                isActive: true,
                createdAt: DateTime.Now,
                updatedAt: null
            );

            order.SetAddress(address);

            _orderRepository.Update(order);
            await _orderRepository.SaveAsync(cancellationToken);

            return OperationResult.Success();
        }
    }
}