using Common.Aplication;
using Common.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.RateLimiting;
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
    [EnableRateLimiting("ApiPolicy")]
    public class UserController : ApiController
    {
        private readonly IUserFacade _userFacade;

        public UserController(IUserFacade userFacade)
        {
            _userFacade = userFacade;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<ApiResult> Register([FromBody] RegisterUserCommand command)
        {
            var result = await _userFacade.Register(command);
            return CommandResult(result);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResult<UserDto>> GetUserById(Guid id)
        {
            var user = await _userFacade.GetUserById(id);
            return QueryResult(user);
        }

        [HttpGet("email/{email}")]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResult<UserDto>> GetUserByEmail(string email)
        {
            var user = await _userFacade.GetUserByEmail(email);
            return QueryResult(user);
        }

        [HttpGet("phone/{phoneNumber}")]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResult<UserDto>> GetUserByPhoneNumber(string phoneNumber)
        {
            var user = await _userFacade.GetUserByPhoneNumber(phoneNumber);
            return QueryResult(user);
        }

        [HttpGet("filter")]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResult<UserFilterData>> GetUsersByFilter(
            [FromQuery] UserFilterParams filterParams)
        {
            var result = await _userFacade.GetUsersByFilter(filterParams);
            return QueryResult(result);
        }

        [HttpPut("edit")]
        [Authorize]
        public async Task<ApiResult> Edit([FromBody] EditUserCommand command)
        {
            var commandWithUserId = command with { Id = UserId };
            var result = await _userFacade.Edit(commandWithUserId);
            return CommandResult(result);
        }

        [HttpPut("change-password")]
        [Authorize]
        public async Task<ApiResult> ChangePassword([FromBody] ChangeUserPasswordCommand command)
        {
            var commandWithUserId = command with { UserId = UserId };
            var result = await _userFacade.ChangePassword(commandWithUserId);
            return CommandResult(result);
        }

        [HttpGet("addresses")]
        [Authorize]
        public async Task<ApiResult<List<UserAddressDto>>> GetMyAddresses()
        {
            var addresses = await _userFacade.GetUserAddresses(UserId);
            return QueryResult(addresses);
        }

        [HttpPost("addresses")]
        [Authorize]
        public async Task<ApiResult> AddAddress([FromBody] AddUserAddressCommand command)
        {
            var commandWithUserId = command with { UserId = UserId };
            var result = await _userFacade.AddAddress(commandWithUserId);
            return CommandResult(result);
        }

        [HttpPut("addresses")]
        [Authorize]
        public async Task<ApiResult> EditAddress([FromBody] EditUserAddressCommand command)
        {
            var commandWithUserId = command with { UserId = UserId };
            var result = await _userFacade.EditAddress(commandWithUserId);
            return CommandResult(result);
        }

        [HttpDelete("addresses/{addressId}")]
        [Authorize]
        public async Task<ApiResult> RemoveAddress(Guid addressId)
        {
            var command = new RemoveUserAddressCommand(UserId, addressId);
            var result = await _userFacade.RemoveAddress(command);
            return CommandResult(result);
        }

        [HttpGet("wallets")]
        [Authorize]
        public async Task<ApiResult<List<WalletDto>>> GetMyWallets()
        {
            var wallets = await _userFacade.GetWalletsByUserId(UserId);
            return QueryResult(wallets);
        }

        [HttpPost("wallets/charge")]
        [Authorize]
        public async Task<ApiResult> ChargeWallet([FromBody] ChargeUserWalletCommand command)
        {
            var commandWithUserId = command with { UserId = UserId };
            var result = await _userFacade.ChargeWallet(commandWithUserId);
            return CommandResult(result);
        }

        [HttpPut("{userId}/set-roles")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiResult>> SetRoles(Guid userId, [FromBody] SetUserRolesCommand command)
        {
            if (userId != command.UserId)
                return BadRequest(new { message = "Id in URL does not match UserId in body" });

            var result = await _userFacade.SetRoles(command);
            return CommandResult(result);
        }
    }
}