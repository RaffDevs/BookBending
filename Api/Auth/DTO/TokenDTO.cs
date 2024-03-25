using System.Security.Claims;

namespace Api.Auth.DTO;

public class TokenDTO
{
    public string? AcessToken { get; set; }
    public string? RefershToken { get; set; }
    
    public ClaimsPrincipal? PrincipalClaims { get; set; }
    public DateTime? ValidTo { get; set; }
}