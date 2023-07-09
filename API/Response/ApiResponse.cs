namespace API.Response
{
    public class ApiResponse
    {
        public bool Successful => ErrorMessage == null;

        public string ErrorMessage { get; set; }

        public object WarningResult { get; set; }

        public object ErrorResult { get; set; } 

        public object Result { get; set; }
        

    }

    public class ApiResponse<TResult, TWarningResult, TErrorResult> : ApiResponse
    {
        public new TResult Result { get => (TResult)base.Result; set => base.Result = value; }

        public new TWarningResult WarningResult { get => (TWarningResult)base.WarningResult; set => base.WarningResult = value; }

        public new TErrorResult ErrorResult { get => (TErrorResult)base.ErrorResult; set => base.ErrorResult = value; }
    }
}
