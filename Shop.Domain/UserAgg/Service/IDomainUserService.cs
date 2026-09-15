using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Domain.UserAgg.Service
{
    public interface IDomainUserService
    {
        bool IsEmailExist(string email);
        bool IsPhoneNumberExist(string phoneNumber);
    }
}
