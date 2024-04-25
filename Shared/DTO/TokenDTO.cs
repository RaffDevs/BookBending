using System.Security.Claims;
using System.Text.Json.Serialization;

namespace Shared.DTO;

public class TokenDTO
{
    public string? AcessToken { get; set; }
    public string? RefreshToken { get; set; }
    
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public DateTime? ValidTo { get; set; }
    
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Username { get; set; }
    
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ClaimsPrincipal? PrincipalClaims { get; set; }
    
}