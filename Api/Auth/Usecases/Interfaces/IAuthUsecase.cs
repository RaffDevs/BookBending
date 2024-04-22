using Api.Auth.DTO;
using Api.Auth.Models;

namespace Api.Auth.Usecases.Interfaces;

public interface IAuthUsecase
{
    public Task<string> CreateRole(string roleName);
    public Task<AddUserToRoleDTO> AddUserToRole(AddUserToRoleDTO data);
    public Task<TokenDTO> Login(LoginDTO data);
    public Task Register(RegisterDTO data);
    public Task<TokenDTO> RefereshToken(TokenDTO data);
    public Task Revoke(string userName);
    public Task<ApplicationUser> Delete(string email);

}