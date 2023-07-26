using System.Runtime.Serialization;

namespace Core.Entities.OrderAggregate
{
    public enum PaymentStatus
    {
        [EnumMember(Value ="Pending")]
        Pending,

        [EnumMember(Value = "Successful")]
        Successful,

        [EnumMember(Value = "Failed")]
        Failed
    }
}
