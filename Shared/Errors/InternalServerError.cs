namespace Shared.Errors;

public sealed class InternalServerError : BaseError
{
    public InternalServerError(int statusCode, string? description) : base(statusCode, description)
    {
    }

    public InternalServerError(int statusCode, string? description, Exception? exception) : base(statusCode,
        description, exception)
    {
    }


    public static InternalServerError Builder(string? message, Exception? exception)
    {
        const int statusCode = 500;
        var description = message ?? "An unexpected error occurred!";

        return exception != null
            ? new InternalServerError(statusCode, description, exception)
            : new InternalServerError(statusCode, description);
    }
}