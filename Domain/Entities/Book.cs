﻿namespace Domain.Entities;

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
    public int BookCaseId { get; private set; }
    public BookCase? BookCase { get; private set; }
    
}