using System.ComponentModel.DataAnnotations;

namespace Shared.DTO;

public class RegisterDTO
{
    [Required(ErrorMessage = "User name is required")]
    [MinLength(3)]
    [MaxLength(15)]
    public string? UserName { get; set; }
    
    [EmailAddress]
    [Required(ErrorMessage = "Email is required")]
    public string? Email { get; set; }
    
    [Required(ErrorMessage = "Password is required")]
    [MinLength(6)]
    public string? Password { get; set; }
}