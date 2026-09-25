using Shop.Query.User.DTOs;
using System;
using System.Collections.Generic;

namespace Shop.Query.User.GetWalletsByUserId
{
    public record GetWalletsByUserIdQuery(Guid UserId) : IBaseQuery<List<WalletDto>>;
}