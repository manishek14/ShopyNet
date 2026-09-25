namespace Shop.Query.Order.DTOs
{
    public class ShippingMethodDto
    {
        public string ShippingType { get; set; } = string.Empty;
        public int ShippingCost { get; set; }
    }
}