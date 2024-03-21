using Microsoft.AspNetCore.Identity;

namespace Api.Auth.Models;

public class ApplicationUser : IdentityUser
{
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpireTime { get; set; }
}