using Common.Query;
using System;

namespace Shop.Query.User.DTOs
{
    public class UserRoleDto : BaseDto
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
    }
}