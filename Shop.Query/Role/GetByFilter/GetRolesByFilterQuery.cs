using Shop.Query.Role.DTOs;

namespace Shop.Query.Role.GetByFilter
{
    public record GetRolesByFilterQuery(RoleFilterParams FilterParams)
        : IBaseQuery<RoleFilterData>;
}