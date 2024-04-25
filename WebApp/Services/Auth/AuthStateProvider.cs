using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace WebApp.Services.Auth;

public class AuthStateProvider : AuthenticationStateProvider
{
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var user = new ClaimsIdentity(new List<Claim>()
        {
            new Claim(ClaimTypes.Name, "Rafael Veronez"),
            new Claim(ClaimTypes.Role, "Admin")
        }, "test");
        
        return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(user)));
    }
}