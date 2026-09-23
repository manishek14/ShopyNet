using Common.Aplication;
using Common.Application;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Application.User.AddAddress
{
    public record AddUserAddressCommand(
        Guid UserId,
        string Province,
        string City,
        string PostalCode,
        string MailingAddress,
        string PhoneNumber,
        string Name,
        string Family,
        string NationalCode
    ) : IBaseCommand;
}