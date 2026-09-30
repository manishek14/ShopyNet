using Common.Aplication;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Sellers.AddInventory;
using Shop.Application.Sellers.ChangeStatus;
using Shop.Application.Sellers.Create;
using Shop.Application.Sellers.Edit;
using Shop.Application.Sellers.EditInventory;
using Shop.Application.Sellers.RemoveInventory;
using Shop.Presentation.Facade.Seller;
using Shop.Query.Seller.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SellerController : ControllerBase
    {
        private readonly ISellerFacade _sellerFacade;

        public SellerController(ISellerFacade sellerFacade)
        {
            _sellerFacade = sellerFacade;
        }

        // Query
        [HttpGet("{id}")]
        public async Task<ActionResult<SellerDto>> GetSellerById(Guid id)
        {
            var seller = await _sellerFacade.GetSellerById(id);
            if (seller == null) return NotFound(new { message = "Seller not found" });
            return Ok(seller);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<SellerDto>> GetSellerByUserId(Guid userId)
        {
            var seller = await _sellerFacade.GetSellerByUserId(userId);
            if (seller == null) return NotFound(new { message = "Seller not found" });
            return Ok(seller);
        }

        [HttpGet("{sellerId}/inventories")]
        public async Task<ActionResult<List<SellerInventoryDto>>> GetSellerInventories(Guid sellerId)
        {
            var inventories = await _sellerFacade.GetSellerInventories(sellerId);
            return Ok(inventories);
        }

        [HttpGet("filter")]
        public async Task<ActionResult<SellerFilterData>> GetSellersByFilter(
            [FromQuery] SellerFilterParams filterParams)
        {
            var result = await _sellerFacade.GetSellersByFilter(filterParams);
            return Ok(result);
        }

        // Command
        [HttpPost]
        public async Task<ActionResult<OperationResult>> CreateSeller([FromBody] CreateSellerCommand command)
        {
            var result = await _sellerFacade.Create(command);
            return result.Status == OperationResultStatus.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<OperationResult>> EditSeller(
            Guid id, [FromBody] EditSellerCommand command)
        {
            if (id != command.Id)
                return BadRequest(new { message = "Id in URL does not match Id in body" });

            var result = await _sellerFacade.Edit(command);
            return result.Status == OperationResultStatus.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPut("{id}/change-status")]
        public async Task<ActionResult<OperationResult>> ChangeStatus(
            Guid id, [FromBody] ChangeSellerStatusCommand command)
        {
            if (id != command.Id)
                return BadRequest(new { message = "Id in URL does not match Id in body" });

            var result = await _sellerFacade.ChangeStatus(command);
            return result.Status == OperationResultStatus.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPost("add-inventory")]
        public async Task<ActionResult<OperationResult>> AddInventory([FromBody] AddSellerInventoryCommand command)
        {
            var result = await _sellerFacade.AddInventory(command);
            return result.Status == OperationResultStatus.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPut("edit-inventory")]
        public async Task<ActionResult<OperationResult>> EditInventory([FromBody] EditSellerInventoryCommand command)
        {
            var result = await _sellerFacade.EditInventory(command);
            return result.Status == OperationResultStatus.Success ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("remove-inventory")]
        public async Task<ActionResult<OperationResult>> RemoveInventory([FromBody] RemoveSellerInventoryCommand command)
        {
            var result = await _sellerFacade.RemoveInventory(command);
            return result.Status == OperationResultStatus.Success ? Ok(result) : BadRequest(result);
        }
    }
}