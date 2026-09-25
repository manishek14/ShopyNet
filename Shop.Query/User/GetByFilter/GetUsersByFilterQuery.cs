using Shop.Query.User.DTOs;

namespace Shop.Query.User.GetByFilter
{
    public record GetUsersByFilterQuery(UserFilterParams FilterParams)
        : IBaseQuery<UserFilterData>;
}