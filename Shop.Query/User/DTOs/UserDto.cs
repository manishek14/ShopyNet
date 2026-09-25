using Common.Query;
using Shop.Domain.UserAgg.Enums;
using System;
using System.Collections.Generic;

namespace Shop.Query.User.DTOs
{
    public class UserDto : BaseDto
    {
        public string Name { get; set; } = string.Empty;
        public string Family { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public Gender Gender { get; set; }
        public bool IsActive { get; set; }
        public List<UserRoleDto> UserRoles { get; set; } = new();
        public List<UserAddressDto> UserAddresses { get; set; } = new();
    }
}