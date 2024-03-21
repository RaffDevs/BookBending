using Domain.Entities;

namespace Domain;

public sealed class BookCase
{
    public int Id { get; private set; }
    public string? Label { get; private set; }
    public BookOwner? Owner { get; private set; }

    public BookCase(string? label, BookOwner? owner)
    {
        Label = label;
        Owner = owner;
    }

    public BookCase(int id, string? label, BookOwner? owner)
    {
        Id = id;
        Label = label;
        Owner = owner;
    }
}