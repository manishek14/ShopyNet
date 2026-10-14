using Common.Aplication;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Application.User.GenerateEmailToken
{
    public record GenerateEmailConfirmationTokenCommand(Guid UserId) : IBaseCommand;
}