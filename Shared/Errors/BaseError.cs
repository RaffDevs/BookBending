namespace Shared.Errors;

public abstract class BaseError : Exception
{
    protected int StatusCode { get; set; }
    protected string? Description { get;  set; }
    protected Exception? Exception { get; set; }

    protected BaseError(int statusCode, string? description) : base(description)
    {
        StatusCode = statusCode;
        Description = description;
    }
    
    protected BaseError(int statusCode, string? description, Exception? exception) : base(description, exception)
    {
        StatusCode = statusCode;
        Description = description;
        Exception = exception;
    }
}