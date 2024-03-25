namespace Shared.Errors;

public sealed class UnauthorizedError : BaseError
{
    private UnauthorizedError(int statusCode, string? description) : base(statusCode, description)
    {
    }

    private UnauthorizedError(int statusCode, string? description, Exception? exception) : base(statusCode, description, exception)
    {
    }

    public static UnauthorizedError Builder(string? message, Exception? exception)
    {
        const int statusCode = 401;
        var description = message ?? "Not allowed";

        return exception != null
            ? new UnauthorizedError(statusCode, description, exception)
            : new UnauthorizedError(statusCode, description);
    }
}