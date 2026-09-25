using Shop.Query.User.DTOs;

namespace Shop.Query.User.GetByPhoneNumber
{
    public record GetUserByPhoneNumberQuery(string PhoneNumber) : IBaseQuery<UserDto>;
}