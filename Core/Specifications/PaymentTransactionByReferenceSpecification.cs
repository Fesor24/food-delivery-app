using Core.Entities;

namespace Core.Specifications
{
    public class PaymentTransactionByReferenceSpecification : BaseSpecification<PaymentTransaction>
    {
        public PaymentTransactionByReferenceSpecification(string trxref) : base(x => x.Reference == trxref)
        {

        }
    }
}
