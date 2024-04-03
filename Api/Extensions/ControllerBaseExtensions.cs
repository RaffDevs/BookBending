using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Shared.Errors;

namespace Api.Extensions;

public static class ControllerBaseExtensions
{
    public static void ValidateUsermameClaim(this ControllerBase controllerBase, string usernameClaim, string userName)
    {
        if (usernameClaim != userName)
        {
            throw BadRequestError.Builder("Invalid user",null);
        }
    }

    public static string ExtractUsernameClaim(this ControllerBase controllerBase)
    {
        var usernameClaim = controllerBase.HttpContext.User.Claims
            .FirstOrDefault(c => c.Type == ClaimTypes.Name);
        
        if (usernameClaim is null)
        {
            throw UnauthorizedError.Builder("No user credentials has been provided", null);
        }

        return usernameClaim.Value;
    }
}