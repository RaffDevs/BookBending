namespace Shared.Errors;

public sealed class ForbiddenError : BaseError
{
    private ForbiddenError(int statusCode, string? description) : base(statusCode, description)
    {
    }

    private ForbiddenError(int statusCode, string? description, Exception? exception) : base(statusCode, description, exception)
    {
    }

    public static ForbiddenError Builder(string? message, Exception? exception)
    {
        const int statusCode = 403;
        var description = message ?? "Forbidden";

        return exception != null
            ? new ForbiddenError(statusCode, description, exception)
            : new ForbiddenError(statusCode, description);
    }
}