namespace Shared.Errors;

public sealed class NotFoundError : BaseError
{
    private NotFoundError(int statusCode, string? description) : base(statusCode, description)
    {
    }

    private NotFoundError(int statusCode, string? description, Exception? exception) : base(statusCode, description,
        exception)
    {
    }

    public static NotFoundError Builder(string? message, Exception? exception)
    {
        const int statusCode = 404;
        var description = message ?? "No records found!";

        return exception != null
            ? new NotFoundError(statusCode, description, exception)
            : new NotFoundError(statusCode, description);
    } 
}