﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Api.Auth.DTO;
using Api.Auth.Models;
using Api.Auth.Services;
using Microsoft.AspNetCore.Identity;
using Shared.Errors;

namespace Api.Auth.Usecases;

//TODO("Criar erros personalizados")

public class AuthUsecase : IAuthUsecase
{
    private readonly ITokenService _tokenService;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _configuration;

    public AuthUsecase(ITokenService tokenService, UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager, IConfiguration configuration)
    {
        _tokenService = tokenService;
        _userManager = userManager;
        _roleManager = roleManager;
        _configuration = configuration;
    }

    public async Task<string> CreateRole(string roleName)
    {
        var roleExists = await _roleManager.RoleExistsAsync(roleName);

        if (!roleExists)
        {
            var roleResult = await _roleManager.CreateAsync(new IdentityRole(roleName));

            if (roleResult.Succeeded)
            {
                return roleName;
            }

            if (roleResult.Errors.Any())
            {
                var errors = string.Join("; ", roleResult.Errors.Select(err => err.Description));
                throw BadRequestError.Builder(errors, null);
            }
        }

        throw ConflictError.Builder("This role already exists", null);
    }

    public async Task<AddUserToRoleDTO> AddUserToRole(AddUserToRoleDTO data)
    {
        var user = await _userManager.FindByEmailAsync(data.Email!);

        if (user != null)
        {
            var result = await _userManager.AddToRoleAsync(user, data.RoleName!);

            if (result.Succeeded)
            {
                return data;
            }

            if (result.Errors.Any())
            {
                var errors = string.Join("; ", result.Errors.Select(err => err.Description));
                throw BadRequestError.Builder(errors, null);
            }
        }

        throw NotFoundError.Builder("No users find for this email", null);
    }

    public async Task<TokenDTO> Login(LoginDTO data)
    {
        var user = await _userManager.FindByNameAsync(data.UserName!);
        if (user is not null && await _userManager.CheckPasswordAsync(user, data.Password!))
        {
            var userRoles = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName!),
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim("id", data.UserName!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            authClaims.AddRange(userRoles.Select(userRole =>
                new Claim(ClaimTypes.Role, userRole))
            );

            var token = _tokenService.GenerateAccessToken(authClaims, _configuration);
            var refreshToken = _tokenService.GenerateRefreshToken();
            _ = int.TryParse(_configuration.GetSection("JWT")
                .GetValue<string>("RefreshTokenValidityInMinutes"), out var refreshTokenValidityInMinutes);

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpireTime = DateTime.UtcNow.AddMinutes(refreshTokenValidityInMinutes);

            await _userManager.UpdateAsync(user);

            return new TokenDTO
            {
                AcessToken = new JwtSecurityTokenHandler().WriteToken(token),
                RefershToken = refreshToken,
                ValidTo = token.ValidTo
            };
        }

        throw UnauthorizedError.Builder("Not allowed here!", null);
    }

    public async Task Register(RegisterDTO data)
    {
        var userExists = await _userManager.FindByNameAsync(data.UserName!);

        if (userExists != null)
        {
            throw ConflictError.Builder("This username already exists", null);
        }

        ApplicationUser user = new ApplicationUser
        {
            Email = data.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = data.UserName
        };

        var result = await _userManager.CreateAsync(user, data.Password!);

        if (!result.Succeeded && result.Errors.Any())
        {
            var errors = string.Join("; ", result.Errors.Select(err => err.Description));
            throw BadRequestError.Builder(errors, null);
        }
    }

    public async Task<TokenDTO> RefereshToken(TokenDTO data)
    {
        if (data is null)
        {
            throw BadRequestError.Builder("Request body must be provided", null);
        }

        var accessToken = data.AcessToken ?? throw new ArgumentNullException(nameof(data));
        var refreshToken = data.RefershToken ?? throw new ArgumentNullException(nameof(data));
        var principalClaims = _tokenService.GetPrincipalFromExpiredToken(accessToken!, _configuration);

        if (principalClaims == null)
        {
            throw BadRequestError.Builder("Invalid access token/refresh token", null);
        }

        var userName = principalClaims.Identity.Name;
        var user = await _userManager.FindByNameAsync(userName!);

        if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpireTime <= DateTime.Now)
        {
            throw BadRequestError.Builder("Invalid access token/refresh token", null);
        }

        var newAccessToken = _tokenService.GenerateAccessToken(principalClaims.Claims.ToList(), _configuration);
        var newRefreshToken = _tokenService.GenerateRefreshToken();

        return new TokenDTO
        {
            AcessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
            RefershToken = newRefreshToken
        };
    }

    public async Task Revoke(string userName)
    {
        var user = await _userManager.FindByNameAsync(userName);

        if (user is null) throw NotFoundError.Builder("No user founded", null);

        user.RefreshToken = null;
        await _userManager.UpdateAsync(user);
    }
}