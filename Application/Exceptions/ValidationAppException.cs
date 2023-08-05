namespace Application.Exceptions;
public sealed class ValidationAppException : Exception
{
    public IReadOnlyDictionary<string, string[]> Errors { get; }

	public ValidationAppException(IReadOnlyDictionary<string, string[]> errors) : 
		base("One or more alidation errors occurred")
	{
		Errors = errors;
	}
}
