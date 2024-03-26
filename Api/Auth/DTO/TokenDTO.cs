using System.Security.Claims;
using System.Text.Json.Serialization;

namespace Api.Auth.DTO;

public class TokenDTO
{
    public string? AcessToken { get; set; }
    public string? RefershToken { get; set; }
    
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public DateTime? ValidTo { get; set; }
    
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Username { get; set; }
    
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ClaimsPrincipal? PrincipalClaims { get; set; }
}