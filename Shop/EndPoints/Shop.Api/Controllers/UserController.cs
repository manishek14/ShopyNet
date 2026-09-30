using Common.Aplication;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.User.AddAddress;
using Shop.Application.User.ChangePassword;
using Shop.Application.User.ChargeWallet;
using Shop.Application.User.Edit;
using Shop.Application.User.EditAddress;
using Shop.Application.User.Register;
using Shop.Application.User.RemoveAddress;
using Shop.Application.User.SetRoles;
using Shop.Presentation.Facade.User;
using Shop.Query.User.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserFacade _userFacade;

        public UserController(IUserFacade userFacade)
        {
            _userFacade = userFacade;
        }

        // Query
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUserById(Guid id)
        {
            var user = await _userFacade.GetUserById(id);
            if (user == null) return NotFound(new { message = "User not found" });
            return Ok(user);
        }

        [HttpGet("email/{email}")]
        public async Task<ActionResult<UserDto>> GetUserByEmail(string email)
        {
            var user = await _userFacade.GetUserByEmail(email);
            if (user == null) return NotFound(new { message = "User not found" });
            return Ok(user);
        }

        [HttpGet("phone/{phoneNumber}")]
        public async Task<ActionResult<UserDto>> GetUserByPhoneNumber(string phoneNumber)
        {
            var user = await _userFacade.GetUserByPhoneNumber(phoneNumber);
            if (user == null) return NotFound(new { message = "User not found" });
            return Ok(user);
        }

        [HttpGet("{userId}/wallets")]
        public async Task<ActionResult<List<WalletDto>>> GetWalletsByUserId(Guid userId)
        {
            var wallets = await _userFacade.GetWalletsByUserId(userId);
            return Ok(wallets);
        }

        [HttpGet("{userId}/addresses")]
        public async Task<ActionResult<List<UserAddressDto>>> GetUserAddresses(Guid userId)
        {
            var addresses = await _userFacade.GetUserAddresses(userId);
            return Ok(addresses);
        }

        [HttpGet("filter")]
        public async Task<ActionResult<UserFilterData>> GetUsersByFilter(
            [FromQuery] UserFilterParams filterParams)
        {
            var result = await _userFacade.GetUsersByFilter(filterParams);
            return Ok(result);
        }

        // Command
        [HttpPost("register")]
        public async Task<ActionResult<OperationResult>> Register([FromBody] RegisterUserCommand command)
        {
            var result = await _userFacade.Register(command);
            return result.Status == OperationResultStatus.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<OperationResult>> Edit(
            Guid id, [FromBody] EditUserCommand command)
        {
            if (id != command.Id)
                return BadRequest(new { message = "Id in URL does not match Id in body" });

            var result = await _userFacade.Edit(command);
            return result.Status == OperationResultStatus.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPut("change-password")]
        public async Task<ActionResult<OperationResult>> ChangePassword([FromBody] ChangeUserPasswordCommand command)
        {
            var result = await _userFacade.ChangePassword(command);
            return result.Status == OperationResultStatus.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPost("add-address")]
        public async Task<ActionResult<OperationResult>> AddAddress([FromBody] AddUserAddressCommand command)
        {
            var result = await _userFacade.AddAddress(command);
            return result.Status == OperationResultStatus.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPut("edit-address")]
        public async Task<ActionResult<OperationResult>> EditAddress([FromBody] EditUserAddressCommand command)
        {
            var result = await _userFacade.EditAddress(command);
            return result.Status == OperationResultStatus.Success ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("remove-address")]
        public async Task<ActionResult<OperationResult>> RemoveAddress([FromBody] RemoveUserAddressCommand command)
        {
            var result = await _userFacade.RemoveAddress(command);
            return result.Status == OperationResultStatus.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPost("charge-wallet")]
        public async Task<ActionResult<OperationResult>> ChargeWallet([FromBody] ChargeUserWalletCommand command)
        {
            var result = await _userFacade.ChargeWallet(command);
            return result.Status == OperationResultStatus.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPut("set-roles")]
        public async Task<ActionResult<OperationResult>> SetRoles([FromBody] SetUserRolesCommand command)
        {
            var result = await _userFacade.SetRoles(command);
            return result.Status == OperationResultStatus.Success ? Ok(result) : BadRequest(result);
        }
    }
}