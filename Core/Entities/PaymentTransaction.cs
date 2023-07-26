namespace Core.Entities
{
    public class PaymentTransaction : BaseEntity
    {
        public string UserEmail { get; set; }

        public string OrderId { get; set; }

        public bool Verified { get; set; } = false;

        public string Reference { get; set; }

        public float Amount { get; set; }
    }
}
