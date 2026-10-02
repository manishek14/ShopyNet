using Common.Aplication;
using Common.Aplication.Validation;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Application.User.RemoveToken
{
    public record RemoveUserTokenCommand(
        Guid UserId,
        Guid TokenId
    ) : IBaseCommand;
}