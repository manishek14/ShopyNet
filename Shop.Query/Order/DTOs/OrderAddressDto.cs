namespace Shop.Query.Order.DTOs
{
    public class OrderAddressDto
    {
        public Guid OrderId { get; set; }
        public string Province { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public string MailingAddress { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Family { get; set; } = string.Empty;
        public string NationalCode { get; set; } = string.Empty;
    }
}