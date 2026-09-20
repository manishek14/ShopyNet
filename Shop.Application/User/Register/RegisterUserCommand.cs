using Common.Aplication;
using Common.Application;
using Common.Application.Validation;
using FluentValidation;
using Shop.Domain.UserAgg.Enums;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Application.User.Register
{
    public record RegisterUserCommand(
        string Email,
        string PhoneNumber,
        string Password,
        string ConfirmPassword,
        Shop.Domain.UserAgg.Enums.Gender Gender
    ) : IBaseCommand;
}