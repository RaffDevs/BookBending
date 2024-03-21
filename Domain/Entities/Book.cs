namespace Domain.Entities;

public sealed class Book
{
    public int Id { get; private set; }
    public string? Description { get; private set; }
    public string? Authors { get; private set; }
    public string? Publisher { get; private set; }
    public int PageCount { get; private set; }
    public string? ThumbnailSmallLink { get; private set; }
    public string? ThumbnailLink { get; private set; }
    public string? BookCode { get;  private set; }
    public string? Isbn { get; private set; }
    public BookCase? BookCase { get; private set; }

    public Book(string? description, string? authors, string? publisher, int pageCount, string? thumbnailSmallLink,
        string? thumbnailLink, string? bookCode, string? isbn, BookCase? bookCase)
    {
        Description = description;
        Authors = authors;
        Publisher = publisher;
        PageCount = pageCount;
        ThumbnailSmallLink = thumbnailSmallLink;
        ThumbnailLink = thumbnailLink;
        BookCode = bookCode;
        Isbn = isbn;
        BookCase = bookCase;
    }

    public Book(int id, string? description, string? authors, string? publisher, int pageCount,
        string? thumbnailSmallLink, string? thumbnailLink, string? bookCode, string? isbn, BookCase? bookCase)
    {
        Id = id;
        Description = description;
        Authors = authors;
        Publisher = publisher;
        PageCount = pageCount;
        ThumbnailSmallLink = thumbnailSmallLink;
        ThumbnailLink = thumbnailLink;
        BookCode = bookCode;
        Isbn = isbn;
        BookCase = bookCase;
        BookCase = bookCase;
    }
}