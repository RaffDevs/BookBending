using Api.Auth.DTO;
using Api.Auth.Usecases;
using Api.Domains.Owner.Usecases.Interfaces;
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

        [HttpPost("CreateRole")]
        public async Task<IActionResult> CreateRole(string roleName)
        {
            var result = await _authUsecase.CreateRole(roleName);
            return Ok($"Role {result} has been created!");
        }

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

        [HttpPost("Refresh")]
        public async Task<IActionResult> RefreshToken(TokenDTO token)
        {
            var result = await _authUsecase.RefereshToken(token);
            return Ok(result);
        }

        [HttpPost("Revoke/{username}")]
        public async Task<IActionResult> Revoke(string username)
        {
            await _authUsecase.Revoke(username);
            return NoContent();
        }
    }
}
