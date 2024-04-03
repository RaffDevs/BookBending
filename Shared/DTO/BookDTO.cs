using System.ComponentModel.DataAnnotations;
using Shared.DTO;

namespace Shared.DTO;

public record BookDTO
{
    public int Id { get; init; }
    
    [Required(ErrorMessage = "Description must be provide")]
    [MinLength(20)]
    [MaxLength(200)]
    public string? Description { get; init; }
    
    public string? Authors { get; init; } = "Unknow";
    
    public string? Publisher { get; init; } = "Unknow";
    
    [Required(ErrorMessage = "PageCount must be provide")]
    [Range(1, int.MaxValue, ErrorMessage = "Please enter a value greater than {1}")]
    public int PageCount { get; init; }
    
    public string? ThumbnailSmallLink { get; init; }
    
    public string? ThumbnailLink { get; init; }
    
    public string? BookCode { get;  init; }
    
    [Required(ErrorMessage = "Isbn must be provide")]
    public string? Isbn { get; init; }
    
    public int BookCaseId { get; init; }
    
    public BookCaseDTO? BookCase { get; init; }
}