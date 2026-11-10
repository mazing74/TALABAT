namespace TALABAT.API.DTO_s
{
    public class OrderDto
    {
        public string BasketId { get; set; }
        public int DelvieryMethodId { get; set; }
        public AddressDto ShippingAddress { get; set; }
    }
}
