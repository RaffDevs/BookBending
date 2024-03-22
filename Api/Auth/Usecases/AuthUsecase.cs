using Api.Auth.DTO;
using Api.Auth.Models;
using Api.Auth.Services;
using Microsoft.AspNetCore.Identity;

namespace Api.Auth.Usecases;

//TODO("Criar erros personalizados")

public class AuthUsecase : IAuthUsecase
{
    private readonly ITokenService _tokenService;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _configuration;
    
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
                throw new NotImplementedException();
            }
        }
        throw new NotImplementedException();
    }

    public Task<AddUserToRoleDTO> AddUserToRole(AddUserToRoleDTO data)
    {
        throw new NotImplementedException();
    }

    public Task<TokenDTO> Login(LoginDTO data)
    {
        throw new NotImplementedException();
    }

    public Task Register(RegisterDTO data)
    {
        throw new NotImplementedException();
    }

    public Task<TokenDTO> RefereshToken(TokenDTO data)
    {
        throw new NotImplementedException();
    }

    public Task Revoke()
    {
        throw new NotImplementedException();
    }
}