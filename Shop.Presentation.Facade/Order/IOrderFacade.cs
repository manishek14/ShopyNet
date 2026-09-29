using Common.Aplication;
using Shop.Application.Order.AddItem;
using Shop.Application.Order.DecreaseItemCount;
using Shop.Application.Order.Finally;
using Shop.Application.Order.IncreaseItemCount;
using Shop.Application.Order.RemoveItem;
using Shop.Application.Order.SetAddress;
using Shop.Application.Order.SetShippingMethod;
using Shop.Query.Order.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shop.Presentation.Facade.Order
{
    public interface IOrderFacade
    {
        Task<OperationResult> AddItem(AddOrderItemCommand command);
        Task<OperationResult> RemoveItem(RemoveOrderItemCommand command);
        Task<OperationResult> IncreaseItemCount(IncreaseOrderItemCountCommand command);
        Task<OperationResult> DecreaseItemCount(DecreaseOrderItemCountCommand command);
        Task<OperationResult> SetAddress(SetOrderAddressCommand command);
        Task<OperationResult> SetShippingMethod(SetOrderShippingMethodCommand command);
        Task<OperationResult> Finally(FinallyOrderCommand command);

        Task<OrderDto> GetOrderById(Guid id);
        Task<OrderFilterData> GetOrdersByFilterQuery(OrderFilterParams FilterParams);
    }
}