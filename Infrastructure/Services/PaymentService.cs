using Core.Entities;
using Core.Interfaces;
using Core.Models;
using Microsoft.Extensions.Logging;
using PayStack.Net;

namespace Infrastructure.Services
{
    public class PaymentService : IPaymentService<TransactionInitializeResponse, TransactionVerifyResponse, object>
    {
        private readonly PayStackApi _payStack;

        private readonly IUnitOfWork _unitOfWork;

        private readonly ILogger<PaymentService> _logger;

        public PaymentService(PayStackApi payStack, IUnitOfWork unitOfWork, ILogger<PaymentService> logger)
        {
             _payStack = payStack;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<PaymentResult<TransactionInitializeResponse, object>> InitializeAsync(Payment payment)
        {
            var paymentResult = new PaymentResult<TransactionInitializeResponse, object>
            {
                ErrorMessage = default,
                Result = new TransactionInitializeResponse { },
                Error = new {}
            };

            var request = new TransactionInitializeRequest
            {
                AmountInKobo = (int)payment.Amount * 100,
                Reference = Guid.NewGuid().ToString(),
                Email = payment.Email,
                Currency = payment.Currency,
                CallbackUrl = payment.CallbackUrl
            };

            var transactionResult = _payStack.Transactions.Initialize(request);

            if (transactionResult.Status)
            {
                paymentResult.Result.Data = transactionResult.Data;
                paymentResult.Result.Status = transactionResult.Status;
                paymentResult.Result.Message = transactionResult.Message;
            }
            else
            {
                _logger.LogError(transactionResult.Message);
            }

            PaymentTransaction paymentTransaction = new PaymentTransaction
            {
                Id = Guid.NewGuid().ToString(),
                OrderId = payment.OrderId,
                Amount = payment.Amount,
                Reference = paymentResult.Result.Data.Reference,
                Verified = false,
                UserEmail = payment.Email
            };

            await _unitOfWork.Repository<PaymentTransaction>().AddAsync(paymentTransaction);

            await _unitOfWork.CompleteAsync();

            return paymentResult;
        }

        public async Task<PaymentResult<TransactionVerifyResponse, object>> VerifyAsync(string reference)
        {
            var paymentResult = new PaymentResult<TransactionVerifyResponse, object>();

            var response = _payStack.Transactions.Verify(reference);

            if (response.Status)
            {
                paymentResult.Result = response;
            }
            else
            {
                _logger.LogError(response.Message);
            }

            return await Task.FromResult(paymentResult);
        }
    }
}
