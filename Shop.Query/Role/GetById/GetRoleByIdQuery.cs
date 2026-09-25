using Shop.Query.Role.DTOs;
using System;

namespace Shop.Query.Role.GetById
{
    public record GetRoleByIdQuery(Guid Id) : IBaseQuery<RoleDto>;
}