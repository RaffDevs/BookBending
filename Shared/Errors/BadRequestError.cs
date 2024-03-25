namespace Shared.Errors;

public sealed class BadRequestError : BaseError
{
    private BadRequestError(int statusCode, string? description) : base(statusCode, description)
    {
    }

    private BadRequestError(int statusCode, string? description, Exception? exception) : base(statusCode, description, exception)
    {
    }

    public static BadRequestError Builder(string? message, Exception? exception)
    {
        const int statusCode = 400;
        var description = message ?? "Malformed content request";

        return exception != null
            ? new BadRequestError(statusCode, description, exception)
            : new BadRequestError(statusCode, description);
    }
}