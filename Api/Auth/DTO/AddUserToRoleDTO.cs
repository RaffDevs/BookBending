using System.ComponentModel.DataAnnotations;

namespace Api.Auth.DTO;

public record AddUserToRoleDTO
{
    [Required(ErrorMessage = "Please, provide the email field ")]
    public string? Email { get; set; }
    
    [Required(ErrorMessage = "Please, provide the role name field ")]
    public string? RoleName { get; set; }
};