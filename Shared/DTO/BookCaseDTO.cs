using System.ComponentModel.DataAnnotations;
using Shared.DTO;

namespace Shared.DTO;

public record BookCaseDTO
{
    public int Id { get; init; }
    
    [Required(ErrorMessage = "Label must be provide.")]
    [MinLength(3)]
    [MaxLength(50)]
    public string? Label { get; init; }
    
    public BookOwnerDTO? BookOwner { get; init; }
}