export interface IApiResponse<TResult, TWarningResult, TErrorResult>{
  errorMessage: string,
  errorResult: TErrorResult,
  warningResult: TWarningResult,
  result: TResult
}
