using Domain.Entities;

namespace Domain;

public sealed class BookCase
{
    public int Id { get; private set; }
    public string? Name { get; private set; }
    public BookOwner? Owner { get; private set; }

    public BookCase(string? name, BookOwner? owner)
    {
        Name = name;
        Owner = owner;
    }

    public BookCase(int id, string? name, BookOwner? owner)
    {
        Id = id;
        Name = name;
        Owner = owner;
    }
}