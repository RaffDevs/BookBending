using Api.Auth.DTO;
using Api.Auth.Usecases;
using Api.Domains.Owner.Usecases.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.DTO;

namespace Api.Auth.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthUsecase _authUsecase;
        private readonly IBookOwnerUsecase _bookOwnerUsecase;

        public AuthController(IAuthUsecase authUsecase, IBookOwnerUsecase bookOwnerUsecase)
        {
            _authUsecase = authUsecase;
            _bookOwnerUsecase = bookOwnerUsecase;
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPost("CreateRole")]
        public async Task<IActionResult> CreateRole(string roleName)
        {
            var result = await _authUsecase.CreateRole(roleName);
            return Ok($"Role {result} has been created!");
        }
        
        [Authorize(Policy = "AdminOnly")]
        [HttpPost("AddUserToRole")]
        public async Task<IActionResult> AddUserToRole(AddUserToRoleDTO data)
        {
            var result = await _authUsecase.AddUserToRole(data);
            return Ok(result);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO login)
        {
            var result = await _authUsecase.Login(login);
            return Ok(result);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO register)
        {
            var data = new BookOwnerDTO
            {
                UserName = register.UserName
            };
            
            await _authUsecase.Register(register);
            await _bookOwnerUsecase.Create(data);
            
            return Ok("User created successfully!");
        }

        [Authorize]
        [HttpPost("Refresh")]
        public async Task<IActionResult> RefreshToken(TokenDTO token)
        {
            var result = await _authUsecase.RefereshToken(token);
            return Ok(result);
        }

        [Authorize]
        [HttpPost("Revoke/{username}")]
        public async Task<IActionResult> Revoke(string username)
        {
            await _authUsecase.Revoke(username);
            return NoContent();
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpDelete]
        public async Task<IActionResult> Delete(string email)
        {
            var result = await _authUsecase.Delete(email);
            var user = await _bookOwnerUsecase.GetBy(bo => bo.UserName == result.UserName);
            await _bookOwnerUsecase.Delete(user.Id);
            return NoContent();
        }
    }
}
