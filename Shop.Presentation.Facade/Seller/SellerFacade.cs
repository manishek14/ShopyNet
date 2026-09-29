using Common.Aplication;
using MediatR;
using Shop.Application.Sellers.AddInventory;
using Shop.Application.Sellers.ChangeStatus;
using Shop.Application.Sellers.Create;
using Shop.Application.Sellers.Edit;
using Shop.Application.Sellers.EditInventory;
using Shop.Application.Sellers.RemoveInventory;
using Shop.Query.Seller.DTOs;
using Shop.Query.Seller.GetById;
using Shop.Query.Seller.GetByFilter;
using Shop.Query.Seller.GetByUserId;
using Shop.Query.Seller.GetInventories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shop.Presentation.Facade.Seller
{
    internal class SellerFacade : ISellerFacade
    {
        private readonly IMediator _mediator;

        public SellerFacade(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<OperationResult> Create(CreateSellerCommand command)
            => await _mediator.Send(command);

        public async Task<OperationResult> Edit(EditSellerCommand command)
            => await _mediator.Send(command);

        public async Task<OperationResult> ChangeStatus(ChangeSellerStatusCommand command)
            => await _mediator.Send(command);

        public async Task<OperationResult> AddInventory(AddSellerInventoryCommand command)
            => await _mediator.Send(command);

        public async Task<OperationResult> EditInventory(EditSellerInventoryCommand command)
            => await _mediator.Send(command);

        public async Task<OperationResult> RemoveInventory(RemoveSellerInventoryCommand command)
            => await _mediator.Send(command);

        public async Task<SellerDto> GetSellerById(Guid id)
            => await _mediator.Send(new GetSellerByIdQuery(id));

        public async Task<SellerDto> GetSellerByUserId(Guid userId)
            => await _mediator.Send(new GetSellerByUserIdQuery(userId));

        public async Task<List<SellerInventoryDto>> GetSellerInventories(Guid sellerId)
            => await _mediator.Send(new GetSellerInventoriesQuery(sellerId));

        public async Task<SellerFilterData> GetSellersByFilter(SellerFilterParams filterParams)
            => await _mediator.Send(new GetSellersByFilterQuery(filterParams));
    }
}