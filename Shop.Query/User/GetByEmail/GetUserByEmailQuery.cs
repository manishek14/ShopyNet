using Shop.Query.User.DTOs;

namespace Shop.Query.User.GetByEmail
{
    public record GetUserByEmailQuery(string Email) : IBaseQuery<UserDto>;
}