using Core.Models;

namespace Core.Interfaces
{
    public interface IPaymentService<TInitializeResult, TVerifyResponse, TError>
    {
        Task<PaymentResult<TInitializeResult, TError>> InitializeAsync(Payment payment);

        Task<PaymentResult<TVerifyResponse, TError>> VerifyAsync(string reference);
    }
}
