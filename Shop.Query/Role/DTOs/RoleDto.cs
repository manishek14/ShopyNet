using Common.Query;
using System.Collections.Generic;

namespace Shop.Query.Role.DTOs
{
    public class RoleDto : BaseDto
    {
        public string Title { get; set; } = string.Empty;
        public List<RolePermissionDto> Permissions { get; set; } = new();
    }
}