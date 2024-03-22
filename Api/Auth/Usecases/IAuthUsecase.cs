using Api.Auth.DTO;

namespace Api.Auth.Usecases;

public interface IAuthUsecase
{
    public Task<string> CreateRole(string roleName);
    public Task<AddUserToRoleDTO> AddUserToRole(AddUserToRoleDTO data);
    public Task<TokenDTO> Login(LoginDTO data);
    public Task Register(RegisterDTO data);
    public Task<TokenDTO> RefereshToken(TokenDTO data);
    public Task Revoke();

}