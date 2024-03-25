namespace Shared.Errors;

public sealed class ConflictError : BaseError
{
    private ConflictError(int statusCode, string? description) : base(statusCode, description)
    {
    }

    private ConflictError(int statusCode, string? description, Exception? exception) : base(statusCode, description, exception)
    {
    }


    public static ConflictError Builder(string? message, Exception? exception)
    {
        const int statusCode = 409;
        var description = message ?? "This record already exists!";

        return exception != null
            ? new ConflictError(statusCode, description, exception)
            : new ConflictError(statusCode, description);
    }
}