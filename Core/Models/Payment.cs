namespace Core.Models
{
    public class Payment
    {
        public float Amount { get; set; }

        public string Email { get; set; }

        public string Currency => "NGN";

        public string CallbackUrl { get; set; }

        public string OrderId { get; set; }
    }
}
