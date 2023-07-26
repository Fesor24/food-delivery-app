namespace Application.Dtos
{
    public class OrderDto
    {
        public string CartId { get; set; }

        public AddressDto Address { get; set; }

        public string CallbackUrl { get; set; }
    }
}
