using Common.Domain;
using System;
using System.IO;

namespace Shop.Domain.UserAgg
{
    public class UserAddress : BaseEntity
    {
        private UserAddress() { }

        public UserAddress(
            string province,
            string city,
            string postalCode,
            string mailingAddress,
            string phoneNumber,
            string name,
            string family,
            string nationalCode)
        {
            Guard(
                province,
                city,
                postalCode,
                mailingAddress,
                phoneNumber,
                name,
                family,
                nationalCode
            );

            Province = province;
            City = city;
            PostalCode = postalCode;
            MailingAddress = mailingAddress;
            PhoneNumber = phoneNumber;
            Name = name;
            Family = family;
            NationalCode = nationalCode;
        }

        public Guid UserId { get; internal set; }
        public string Province { get; private set; } = string.Empty;
        public string City { get; private set; } = string.Empty;
        public string PostalCode { get; private set; } = string.Empty;
        public string MailingAddress { get; private set; } = string.Empty;
        public string PhoneNumber { get; private set; } = string.Empty;
        public string Name { get; private set; } = string.Empty;
        public string Family { get; private set; } = string.Empty;
        public string NationalCode { get; private set; } = string.Empty;
        public bool IsActive { get; private set; } = true;
        public DateTime CreatedAt { get; private set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; private set; }

        public void Edit(
            string province,
            string city,
            string postalCode,
            string mailingAddress,
            string phoneNumber,
            string name,
            string family,
            string nationalCode)
        {
            Guard(
                province,
                city,
                postalCode,
                mailingAddress,
                phoneNumber,
                name,
                family,
                nationalCode
            );

            Province = province;
            City = city;
            PostalCode = postalCode;
            MailingAddress = mailingAddress;
            PhoneNumber = phoneNumber;
            Name = name;
            Family = family;
            NationalCode = nationalCode;
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
            string province,
            string city,
            string postalCode,
            string mailingAddress,
            string phoneNumber,
            string name,
            string family,
            string nationalCode)
        {
            NullOrEmptyDomainDataException.CheckString(province, nameof(province));
            NullOrEmptyDomainDataException.CheckString(city, nameof(city));
            NullOrEmptyDomainDataException.CheckString(postalCode, nameof(postalCode));
            NullOrEmptyDomainDataException.CheckString(mailingAddress, nameof(mailingAddress));
            NullOrEmptyDomainDataException.CheckString(phoneNumber, nameof(phoneNumber));
            NullOrEmptyDomainDataException.CheckString(name, nameof(name));
            NullOrEmptyDomainDataException.CheckString(family, nameof(family));
            NullOrEmptyDomainDataException.CheckString(nationalCode, nameof(nationalCode));

            if (postalCode.Length != 10)
                throw new InvalidDataException("Postal code must be 10 digits!");

            if (phoneNumber.Length != 11)
                throw new InvalidDataException("Phone number must be 11 digits!");

            if (nationalCode.Length != 10)
                throw new InvalidDataException("National code must be 10 digits!");
        }
    }
}