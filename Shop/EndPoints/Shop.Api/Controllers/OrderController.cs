using Common.Aplication;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Order.AddItem;
using Shop.Application.Order.DecreaseItemCount;
using Shop.Application.Order.Finally;
using Shop.Application.Order.IncreaseItemCount;
using Shop.Application.Order.RemoveItem;
using Shop.Application.Order.SetAddress;
using Shop.Application.Order.SetShippingMethod;
using Shop.Presentation.Facade.Order;
using Shop.Query.Order.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderFacade _orderFacade;

        public OrderController(IOrderFacade orderFacade)
        {
            _orderFacade = orderFacade;
        }

        // Query
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> GetOrderById(Guid id)
        {
            var order = await _orderFacade.GetOrderById(id);
            if (order == null) return NotFound(new { message = "Order not found" });
            return Ok(order);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<List<OrderDto>>> GetOrdersByUserId(Guid userId)
        {
            var orders = await _orderFacade.GetOrdersByUserId(userId);
            return Ok(orders);
        }

        [HttpGet("filter")]
        public async Task<ActionResult<OrderFilterData>> GetOrdersByFilter(
            [FromQuery] OrderFilterParams filterParams)
        {
            var result = await _orderFacade.GetOrdersByFilter(filterParams);
            return Ok(result);
        }

        // Command
        [HttpPost("add-item")]
        public async Task<ActionResult<OperationResult>> AddItem([FromBody] AddOrderItemCommand command)
        {
            var result = await _orderFacade.AddItem(command);
            return result.Status == OperationResultStatus.Success ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("remove-item")]
        public async Task<ActionResult<OperationResult>> RemoveItem([FromBody] RemoveOrderItemCommand command)
        {
            var result = await _orderFacade.RemoveItem(command);
            return result.Status == OperationResultStatus.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPut("increase-item")]
        public async Task<ActionResult<OperationResult>> IncreaseItemCount([FromBody] IncreaseOrderItemCountCommand command)
        {
            var result = await _orderFacade.IncreaseItemCount(command);
            return result.Status == OperationResultStatus.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPut("decrease-item")]
        public async Task<ActionResult<OperationResult>> DecreaseItemCount([FromBody] DecreaseOrderItemCountCommand command)
        {
            var result = await _orderFacade.DecreaseItemCount(command);
            return result.Status == OperationResultStatus.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPut("set-address")]
        public async Task<ActionResult<OperationResult>> SetAddress([FromBody] SetOrderAddressCommand command)
        {
            var result = await _orderFacade.SetAddress(command);
            return result.Status == OperationResultStatus.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPut("set-shipping")]
        public async Task<ActionResult<OperationResult>> SetShippingMethod([FromBody] SetOrderShippingMethodCommand command)
        {
            var result = await _orderFacade.SetShippingMethod(command);
            return result.Status == OperationResultStatus.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPost("finally")]
        public async Task<ActionResult<OperationResult>> Finally([FromBody] FinallyOrderCommand command)
        {
            var result = await _orderFacade.Finally(command);
            return result.Status == OperationResultStatus.Success ? Ok(result) : BadRequest(result);
        }

    }
}