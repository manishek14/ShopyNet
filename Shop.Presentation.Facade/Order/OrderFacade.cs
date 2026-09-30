using Common.Aplication;
using MediatR;
using Shop.Application.Order.AddItem;
using Shop.Application.Order.DecreaseItemCount;
using Shop.Application.Order.Finally;
using Shop.Application.Order.IncreaseItemCount;
using Shop.Application.Order.RemoveItem;
using Shop.Application.Order.SetAddress;
using Shop.Application.Order.SetShippingMethod;
using Shop.Query.Order.DTOs;
using Shop.Query.Order.GetById;
using Shop.Query.Order.GetByFilter;
using Shop.Query.Order.GetByUserId;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shop.Presentation.Facade.Order
{
    internal class OrderFacade : IOrderFacade
    {
        private readonly IMediator _mediator;

        public OrderFacade(IMediator mediator)
        {
            _mediator = mediator;
        }

        // Command
        public async Task<OperationResult> AddItem(AddOrderItemCommand command)
            => await _mediator.Send(command);

        public async Task<OperationResult> RemoveItem(RemoveOrderItemCommand command)
            => await _mediator.Send(command);

        public async Task<OperationResult> IncreaseItemCount(IncreaseOrderItemCountCommand command)
            => await _mediator.Send(command);

        public async Task<OperationResult> DecreaseItemCount(DecreaseOrderItemCountCommand command)
            => await _mediator.Send(command);

        public async Task<OperationResult> SetAddress(SetOrderAddressCommand command)
            => await _mediator.Send(command);

        public async Task<OperationResult> SetShippingMethod(SetOrderShippingMethodCommand command)
            => await _mediator.Send(command);

        public async Task<OperationResult> Finally(FinallyOrderCommand command)
            => await _mediator.Send(command);


        // Query
        public async Task<OrderDto> GetOrderById(Guid id)
            => await _mediator.Send(new GetOrderByIdQuery(id));

        public async Task<List<OrderDto>> GetOrdersByUserId(Guid userId)
            => await _mediator.Send(new GetOrderByUserIdQuery(userId));

        // Filter
        public async Task<OrderFilterData> GetOrdersByFilter(OrderFilterParams filterParams)
            => await _mediator.Send(new GetOrdersByFilterQuery(filterParams));
    }
}