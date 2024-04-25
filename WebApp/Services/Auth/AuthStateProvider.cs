using System.Net.Http.Headers;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using WebApp.Extensions;

namespace WebApp.Services.Auth;

public class AuthStateProvider : AuthenticationStateProvider
{
    private readonly IJSRuntime _js;
    private readonly HttpClient _httpClient;
    public static readonly string TokenKey = "tokenKey"; 
    public AuthStateProvider(IJSRuntime js, HttpClient client)
    {
        _js = js;
        _httpClient = client;
    }

    private AuthenticationState NotAuthenticated()
    {
        return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
    }

    public AuthenticationState CreateAuthentication(string token)
    {
        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var token = await _js.GetItemInLocalStorage(TokenKey);

        if (string.IsNullOrEmpty(token))
        {
            return NotAuthenticated();
        }

        return CreateAuthentication(token);
    }
}