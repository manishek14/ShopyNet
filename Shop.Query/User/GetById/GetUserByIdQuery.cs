using Shop.Query.User.DTOs;
using System;

namespace Shop.Query.User.GetById
{
    public record GetUserByIdQuery(Guid Id) : IBaseQuery<UserDto>;
}