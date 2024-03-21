using System.ComponentModel.DataAnnotations;

namespace Shared.DTO;

public record BookOwnerDTO
{
    public int Id { get; init; }
    
    [Required(ErrorMessage = "Username must be provide")]
    [MinLength(3)]
    [MaxLength(15)]
    public string? UserName { get; init; }
}