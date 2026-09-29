using Common.Aplication;
using Shop.Application.Sellers.AddInventory;
using Shop.Application.Sellers.ChangeStatus;
using Shop.Application.Sellers.Create;
using Shop.Application.Sellers.Edit;
using Shop.Application.Sellers.EditInventory;
using Shop.Application.Sellers.RemoveInventory;
using Shop.Query.Seller.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shop.Presentation.Facade.Seller
{
    public interface ISellerFacade
    {
        Task<OperationResult> Create(CreateSellerCommand command);
        Task<OperationResult> Edit(EditSellerCommand command);
        Task<OperationResult> ChangeStatus(ChangeSellerStatusCommand command);
        Task<OperationResult> AddInventory(AddSellerInventoryCommand command);
        Task<OperationResult> EditInventory(EditSellerInventoryCommand command);
        Task<OperationResult> RemoveInventory(RemoveSellerInventoryCommand command);

        Task<SellerDto> GetSellerById(Guid id);
        Task<SellerDto> GetSellerByUserId(Guid userId);
        Task<List<SellerInventoryDto>> GetSellerInventories(Guid sellerId);

        Task<SellerFilterData> GetSellersByFilter(SellerFilterParams filterParams);
    }
}