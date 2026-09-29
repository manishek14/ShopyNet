using Common.Aplication;
using MediatR;
using Shop.Application.User.AddAddress;
using Shop.Application.User.ChangePassword;
using Shop.Application.User.ChargeWallet;
using Shop.Application.User.Edit;
using Shop.Application.User.EditAddress;
using Shop.Application.User.Register;
using Shop.Application.User.RemoveAddress;
using Shop.Application.User.SetRoles;
using Shop.Query.User.DTOs;
using Shop.Query.User.GetAddresses;
using Shop.Query.User.GetByEmail;
using Shop.Query.User.GetByFilter;
using Shop.Query.User.GetById;
using Shop.Query.User.GetByPhoneNumber;
using Shop.Query.User.GetWalletsByUserId;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shop.Presentation.Facade.User
{
    internal class UserFacade : IUserFacade
    {
        private readonly IMediator _mediator;

        public UserFacade(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<OperationResult> Register(RegisterUserCommand command)
            => await _mediator.Send(command);

        public async Task<OperationResult> Edit(EditUserCommand command)
            => await _mediator.Send(command);

        public async Task<OperationResult> ChangePassword(ChangeUserPasswordCommand command)
            => await _mediator.Send(command);

        public async Task<OperationResult> AddAddress(AddUserAddressCommand command)
            => await _mediator.Send(command);

        public async Task<OperationResult> EditAddress(EditUserAddressCommand command)
            => await _mediator.Send(command);

        public async Task<OperationResult> RemoveAddress(RemoveUserAddressCommand command)
            => await _mediator.Send(command);

        public async Task<OperationResult> ChargeWallet(ChargeUserWalletCommand command)
            => await _mediator.Send(command);

        public async Task<OperationResult> SetRoles(SetUserRolesCommand command)
            => await _mediator.Send(command);

        public async Task<UserDto> GetUserById(Guid id)
            => await _mediator.Send(new GetUserByIdQuery(id));

        public async Task<UserDto> GetUserByEmail(string email)
            => await _mediator.Send(new GetUserByEmailQuery(email));

        public async Task<UserDto> GetUserByPhoneNumber(string phoneNumber)
            => await _mediator.Send(new GetUserByPhoneNumberQuery(phoneNumber));

        public async Task<List<WalletDto>> GetWalletsByUserId(Guid userId)
            => await _mediator.Send(new GetWalletsByUserIdQuery(userId));

        public async Task<List<UserAddressDto>> GetUserAddresses(Guid userId)
            => await _mediator.Send(new GetUserAddressesQuery(userId));

        public async Task<UserFilterData> GetUsersByFilter(UserFilterParams filterParams)
            => await _mediator.Send(new GetUsersByFilterQuery(filterParams));
    }
}