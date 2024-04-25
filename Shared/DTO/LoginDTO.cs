using System.ComponentModel.DataAnnotations;

namespace Shared.DTO;

public class LoginDTO
{
    [Required(ErrorMessage = "Email is required")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{6,}$",
        ErrorMessage = "Your password must have one uppercase, lowercase and number!")]
    public string? Password { get; set; }
}