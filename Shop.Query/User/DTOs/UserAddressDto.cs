using Common.Query;
using System;

namespace Shop.Query.User.DTOs
{
    public class UserAddressDto : BaseDto
    {
        public Guid UserId { get; set; }
        public string Province { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public string MailingAddress { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Family { get; set; } = string.Empty;
        public string NationalCode { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}