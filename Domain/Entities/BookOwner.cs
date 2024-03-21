namespace Domain.Entities;

public sealed class BookOwner
{
    public int Id { get; private set; }
    public string? UserName { get; private set; }

    public BookOwner(string? userName)
    {
        UserName = userName;
    }

    public BookOwner(int id, string? userName)
    {
        Id = id;
        UserName = userName;
    }
}