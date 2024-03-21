using System.ComponentModel.DataAnnotations;
using Shared.DTO;

namespace Shared.DTO;

public record BookDTO
{
    public int Id { get; private set; }
    [Required(ErrorMessage = "Description must be provide")]
    [MinLength(20)]
    [MaxLength(200)]
    public string? Description { get; private set; }
    public string? Authors { get; private set; } = "Unknow";
    public string? Publisher { get; private set; } = "Unknow";
    [Required(ErrorMessage = "PageCount must be provide")]
    [Range(1, int.MaxValue, ErrorMessage = "Please enter a value greater than {1}")]
    public int PageCount { get; private set; }
    public string? ThumbnailSmallLink { get; private set; }
    public string? ThumbnailLink { get; private set; }
    public string? BookCode { get;  private set; }
    [Required(ErrorMessage = "Isbn must be provide")]
    public string? Isbn { get; private set; }
    public BookCaseDTO? BookCase { get; private set; }
}