using Common.Domain;
using Shop.Domain.UserAgg.Enums;
using Shop.Domain.UserAgg.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Domain.UserAgg
{
    public class User : BaseAggregate
    {
        public User(string name, string family, string email, string phoneNumber, string password, Gender gender)
        {
            Name = name;
            Family = family;
            Email = email;
            PhoneNumber = phoneNumber;
            Password = password;
            Gender = gender;

            // initialize collections to avoid null refs later
            UserRoles = new List<UserRole>();
            Wallets = new List<Wallet>();
            UserAddresses = new List<UserAddress>();
        }

        public string Name { get; private set; }
        public string Family { get; private set; }
        public string Email { get; private set; }
        public string PhoneNumber { get; private set; }
        public string Password { get; private set; }
        public Gender Gender { get; private set; }
        public List<UserRole> UserRoles { get; private set; }
        public List<Wallet> Wallets { get; private set; }
        public List<UserAddress> UserAddresses { get; private set; }

        public void Edit(string name, string family, string email, string phoneNumber, string password, Gender gender, IDomainUserService domainService)
        {
            // Guard expects (phoneNumber, email, domainService)
            Guard(phoneNumber, email, domainService);
            Name = name;
            Family = family;
            Email = email;
            PhoneNumber = phoneNumber;
            Password = password;
            Gender = gender;
        }

        public static User Register(string email, string phoneNumber, string password, IDomainUserService domainService)
        {
            var user = new User("", "", email, phoneNumber, password, default(Gender));

            return user;
        }
        public void AddAddress(UserAddress address)
        {
            address.UserId = Id;
            UserAddresses.Add(address);
        }
        public void EditAddress(UserAddress address)
        {
            var OldAddress = UserAddresses.Find(a => a.Id == address.Id);
            if (OldAddress != null)
                throw new NullOrEmptyDomainDataException("Address not found");

            UserAddresses.Remove(OldAddress);
            UserAddresses.Add(address);
        }
        public void RemoveAddress(UserAddress address)
        {
            var OldAddress = UserAddresses.Find(a => a.Id == address.Id);
            if (OldAddress != null)
                throw new NullOrEmptyDomainDataException("Address not found");

            UserAddresses.Remove(OldAddress);
        }

        public void ChargeWallet(Wallet wallet)
        {
        }
        public void SetRoles(List<UserRole> Role)
        {
            Role.ForEach(f => f.UserId = Id);
            Role.Clear();
            Role.AddRange(Role);
        }

        public void Guard(string phoneNumber, string email, IDomainUserService domainService)
        {
            NullOrEmptyDomainDataException.CheckString(phoneNumber, nameof(phoneNumber));
            NullOrEmptyDomainDataException.CheckString(email, nameof(email));

            if (phoneNumber.Length != 11) throw new InvalidDataException("phone number isn't valid!");

            // validate the parameter 'email', not the instance property
            if (!EmailValidator.IsValidEmail(email)) throw new InvalidDataException("email isn't valid!");

            if (phoneNumber != PhoneNumber)
                if (domainService.IsPhoneNumberExist(phoneNumber)) throw new InvalidDataException("phone number isn't valid!");

            if (email != Email)
                if (domainService.IsEmailExist(email)) throw new InvalidDataException("email isn't valid!");
        }
    }
}
