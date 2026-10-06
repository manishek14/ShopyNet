using Common.Aplication;
using Common.Aplication.Validation;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Application.User.AddToken
{
    // Command
    public record AddUserTokenCommand(
        Guid UserId,
        string HashJwtToken,
        string HashRefreshToken,
        DateTime TokenExpireDate,
        DateTime RefreshTokenExpireDate,
        string Device
    ) : IBaseCommand;
}