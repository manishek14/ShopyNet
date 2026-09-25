using Shop.Query.User.DTOs;
using System;
using System.Collections.Generic;

namespace Shop.Query.User.GetAddresses
{
    public record GetUserAddressesQuery(Guid UserId) : IBaseQuery<List<UserAddressDto>>;
}