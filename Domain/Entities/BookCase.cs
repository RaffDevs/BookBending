using Domain.Entities;

namespace Domain;

public sealed class BookCase
{
    public int Id { get; private set; }
    public string? Label { get; private set; }
    
    public int BookOwnerId { get; private set; }
    public BookOwner? Owner { get; private set; }
    public IEnumerable<Book> Books { get; private set; }
    
}