using Shop.Query.User.DTOs;
using System;

namespace Shop.Query.User.GetWalletById
{
    public record GetWalletByIdQuery(Guid WalletId) : IBaseQuery<WalletDto>;
}