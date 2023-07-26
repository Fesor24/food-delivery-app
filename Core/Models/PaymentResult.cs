using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class PaymentResult
    {
        public string ErrorMessage { get; set; }

        public bool Successful => ErrorMessage == null;

        public object Result { get; set; }

        public object Error { get; set; }
    }

    public class PaymentResult<TResult, TError>: PaymentResult
    {
        public new TResult Result { get => (TResult)base.Result; set => base.Result = value; }

        public new TError Error { get => (TError)base.Error; set => base.Error = value; }
    }
}
