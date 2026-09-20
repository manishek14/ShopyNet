using Common.Aplication;
using Common.Application;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Application.User.ChangePassword
{
    public record ChangeUserPasswordCommand(
        Guid UserId,
        string CurrentPassword,
        string NewPassword,
        string ConfirmNewPassword
    ) : IBaseCommand;
}