using Common.Domain;
using Shop.Domain.UserAgg.Enums;
using Shop.Domain.UserAgg.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Shop.Domain.UserAgg
{
    public class User : BaseAggregate
    {
        // EF Core
        private User() { }

        private User(
            string name,
            string family,
            string email,
            string phoneNumber,
            string password,
            Gender gender)
        {
            Name = name;
            Family = family;
            Email = email;
            PhoneNumber = phoneNumber;
            Password = password;
            Gender = gender;

            UserRoles = new List<UserRole>();
            Wallets = new List<Wallet>();
            UserAddresses = new List<UserAddress>();

            CreatedAt = DateTime.Now;
        }

        public string Name { get; private set; } = string.Empty;
        public string Family { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string PhoneNumber { get; private set; } = string.Empty;
        public string Password { get; private set; } = string.Empty;
        public Gender Gender { get; private set; }
        public List<UserRole> UserRoles { get; private set; }
        public List<Wallet> Wallets { get; private set; }
        public List<UserAddress> UserAddresses { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }
        public bool IsActive { get; private set; } = true;

        public static User Register(
            string email,
            string phoneNumber,
            string password,
            IDomainUserService domainService,
            Gender gender
            )
        {
            Guard(email, phoneNumber, null, null, domainService);

            return new User(
                name: "",
                family: "",
                email: email,
                phoneNumber: phoneNumber,
                password: password,
                gender: gender
            );
        }

        public void Edit(
            string name,
            string family,
            string email,
            string phoneNumber,
            Gender gender,
            IDomainUserService domainService)
        {
            Guard(email, phoneNumber, Email, PhoneNumber, domainService);

            Name = name;
            Family = family;
            Email = email;
            PhoneNumber = phoneNumber;
            Gender = gender;
            UpdatedAt = DateTime.Now;
        }

        public void ChangePassword(string newPassword)
        {
            NullOrEmptyDomainDataException.CheckString(newPassword, nameof(newPassword));

            if (newPassword.Length < 6)
                throw new InvalidDataException("Password must be at least 6 characters.");

            Password = newPassword;
            UpdatedAt = DateTime.Now;
        }

        public void ChargeWallet(Wallet wallet)
        {
            if (wallet == null)
                throw new ArgumentNullException(nameof(wallet));

            wallet.AssignToUser(Id);
            Wallets.Add(wallet);
            UpdatedAt = DateTime.Now;
        }

        public void SetRoles(List<UserRole> roles)
        {
            if (roles == null)
                throw new ArgumentNullException(nameof(roles));

            roles.ForEach(r => r.UserId = Id);

            UserRoles.Clear();
            UserRoles.AddRange(roles);

            UpdatedAt = DateTime.Now;
        }

        public void AddAddress(UserAddress address)
        {
            if (address == null)
                throw new ArgumentNullException(nameof(address));

            address.UserId = Id;
            UserAddresses.Add(address);
            UpdatedAt = DateTime.Now;
        }

        public void EditAddress(UserAddress address)
        {
            if (address == null)
                throw new ArgumentNullException(nameof(address));

            var oldAddress = UserAddresses.FirstOrDefault(a => a.Id == address.Id);

            if (oldAddress == null)
                throw new NullOrEmptyDomainDataException("Address not found!");

            UserAddresses.Remove(oldAddress);
            address.UserId = Id;
            UserAddresses.Add(address);
            UpdatedAt = DateTime.Now;
        }

        public void RemoveAddress(Guid addressId)
        {
            if (addressId == Guid.Empty)
                throw new ArgumentException("AddressId cannot be empty.", nameof(addressId));

            var oldAddress = UserAddresses.FirstOrDefault(a => a.Id == addressId);

            if (oldAddress == null)
                throw new NullOrEmptyDomainDataException("Address not found!");

            UserAddresses.Remove(oldAddress);
            UpdatedAt = DateTime.Now;
        }

        public void Activate()
        {
            IsActive = true;
            UpdatedAt = DateTime.Now;
        }

        public void Deactivate()
        {
            IsActive = false;
            UpdatedAt = DateTime.Now;
        }

        private static void Guard(
            string email,
            string phoneNumber,
            string? currentEmail,
            string? currentPhoneNumber,
            IDomainUserService domainService)
        {
            NullOrEmptyDomainDataException.CheckString(email, nameof(email));
            NullOrEmptyDomainDataException.CheckString(phoneNumber, nameof(phoneNumber));

            if (phoneNumber.Length != 11)
                throw new InvalidDataException("Phone number is not valid!");

            if (!EmailValidator.IsValidEmail(email))
                throw new InvalidDataException("Email is not valid!");

            if (currentPhoneNumber != phoneNumber)
            {
                if (domainService.IsPhoneNumberExist(phoneNumber))
                    throw new InvalidDataException("Phone number already exists!");
            }

            if (currentEmail != email)
            {
                if (domainService.IsEmailExist(email))
                    throw new InvalidDataException("Email already exists!");
            }
        }


    }
}