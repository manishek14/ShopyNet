using Common.Aplication;
using Common.Application;
using Shop.Domain.UserAgg.Enums;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Application.User.Edit
{
    public record EditUserCommand(
        Guid Id,
        string Name,
        string Family,
        string Email,
        string PhoneNumber,
        Gender Gender
    ) : IBaseCommand;
}