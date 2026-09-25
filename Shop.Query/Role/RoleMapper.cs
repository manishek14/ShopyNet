using Shop.Query.Role.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace Shop.Query.Role
{
    internal static class RoleMapper
    {
        public static RoleDto? Map(this Domain.RoleAgg.Role? role)
        {
            if (role is null)
                return null;

            return new RoleDto
            {
                Id = role.Id,
                Title = role.Title,
                CreatedDate = role.CreatedAt,
                UpdatedDate = role.UpdatedAt,
                Permissions = role.Permissions?.Select(MapPermission).ToList()
                              ?? new List<RolePermissionDto>()
            };
        }

        public static List<RoleDto> MapList(this List<Domain.RoleAgg.Role>? roles)
        {
            var result = new List<RoleDto>();
            if (roles == null)
                return result;

            foreach (var role in roles)
            {
                result.Add(Map(role)!);
            }

            return result;
        }

        private static RolePermissionDto MapPermission(Domain.RoleAgg.Role.RolePermission permission)
        {
            return new RolePermissionDto
            {
                Id = permission.Id,
                RoleId = permission.RoleId,
                Permission = permission.Permission
            };
        }
    }
}