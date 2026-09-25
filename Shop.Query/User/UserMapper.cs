using Shop.Query.User.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace Shop.Query.User
{
    internal static class UserMapper
    {
        public static UserDto? Map(this Domain.UserAgg.User? user)
        {
            if (user is null)
                return null;

            return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Family = user.Family,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Gender = user.Gender,
                IsActive = user.IsActive,
                CreatedDate = user.CreatedAt,
                UpdatedDate = user.UpdatedAt,
                UserRoles = user.UserRoles?.Select(MapRole).ToList() ?? new List<UserRoleDto>(),
                UserAddresses = user.UserAddresses?.Select(MapAddress).ToList() ?? new List<UserAddressDto>()
            };
        }

        public static List<UserDto> MapList(this List<Domain.UserAgg.User>? users)
        {
            var result = new List<UserDto>();
            if (users == null)
                return result;

            foreach (var user in users)
            {
                result.Add(new UserDto
                {
                    Id = user.Id,
                    Name = user.Name,
                    Family = user.Family,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Gender = user.Gender,
                    IsActive = user.IsActive,
                    CreatedDate = user.CreatedAt,
                    UpdatedDate = user.UpdatedAt,
                    UserRoles = new List<UserRoleDto>(),
                    UserAddresses = new List<UserAddressDto>()
                });
            }

            return result;
        }

        public static WalletDto Map(this Domain.UserAgg.Wallet wallet)
        {
            return new WalletDto
            {
                Id = wallet.Id,
                UserId = wallet.UserId,
                Price = wallet.Price,
                Description = wallet.Description,
                Type = wallet.Type,
                IsFinally = wallet.IsFinally,
                FinallyAt = wallet.FinallyAt,
                CreatedDate = wallet.CreatedAt
            };
        }

        public static List<WalletDto> MapList(this List<Domain.UserAgg.Wallet>? wallets)
        {
            var result = new List<WalletDto>();
            if (wallets == null)
                return result;

            foreach (var wallet in wallets)
                result.Add(Map(wallet));

            return result;
        }

        private static UserRoleDto MapRole(Domain.UserAgg.UserRole userRole)
        {
            return new UserRoleDto
            {
                Id = userRole.Id,
                UserId = userRole.UserId,
                RoleId = userRole.RoleId
            };
        }

        private static UserAddressDto MapAddress(Domain.UserAgg.UserAddress address)
        {
            return new UserAddressDto
            {
                Id = address.Id,
                UserId = address.UserId,
                Province = address.Province,
                City = address.City,
                PostalCode = address.PostalCode,
                MailingAddress = address.MailingAddress,
                PhoneNumber = address.PhoneNumber,
                Name = address.Name,
                Family = address.Family,
                NationalCode = address.NationalCode,
                IsActive = address.IsActive,
                CreatedDate = address.CreatedAt,
                UpdatedDate = address.UpdatedAt
            };
        }
    }
}