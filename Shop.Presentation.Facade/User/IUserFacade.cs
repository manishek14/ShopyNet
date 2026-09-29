using Common.Aplication;
using Shop.Application.User.AddAddress;
using Shop.Application.User.ChangePassword;
using Shop.Application.User.ChargeWallet;
using Shop.Application.User.Edit;
using Shop.Application.User.EditAddress;
using Shop.Application.User.Register;
using Shop.Application.User.RemoveAddress;
using Shop.Application.User.SetRoles;
using Shop.Query.User.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shop.Presentation.Facade.User
{
    public interface IUserFacade
    {
        Task<OperationResult> Register(RegisterUserCommand command);
        Task<OperationResult> Edit(EditUserCommand command);
        Task<OperationResult> ChangePassword(ChangeUserPasswordCommand command);
        Task<OperationResult> AddAddress(AddUserAddressCommand command);
        Task<OperationResult> EditAddress(EditUserAddressCommand command);
        Task<OperationResult> RemoveAddress(RemoveUserAddressCommand command);
        Task<OperationResult> ChargeWallet(ChargeUserWalletCommand command);
        Task<OperationResult> SetRoles(SetUserRolesCommand command);

        Task<UserDto> GetUserById(Guid id);
        Task<UserDto> GetUserByEmail(string email);
        Task<UserDto> GetUserByPhoneNumber(string phoneNumber);
        Task<List<WalletDto>> GetWalletsByUserId(Guid userId);
        Task<List<UserAddressDto>> GetUserAddresses(Guid userId);

        Task<UserFilterData> GetUsersByFilter(UserFilterParams filterParams);
    }
}