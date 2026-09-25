using Common.Query;
using Shop.Domain.RoleAgg;
using System;
using DomainRole = Shop.Domain.RoleAgg.Role;

namespace Shop.Query.Role.DTOs
{
    public class RolePermissionDto : BaseDto
    {
        public Guid RoleId { get; set; }
        public DomainRole.Permission Permission { get; set; }
    }
}