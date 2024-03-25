using Api.Auth.DTO;
using Api.Auth.Usecases;
using Microsoft.AspNetCore.Mvc;

namespace Api.Auth.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthUsecase _usecase;

        public AuthController(IAuthUsecase usecase)
        {
            _usecase = usecase;
        }

        [HttpPost("CreateRole")]
        public async Task<IActionResult> CreateRole(string roleName)
        {
            var result = await _usecase.CreateRole(roleName);
            return Ok($"Role {result} has been created!");
        }

        [HttpPost("AddUserToRole")]
        public async Task<IActionResult> AddUserToRole(AddUserToRoleDTO data)
        {
            var result = await _usecase.AddUserToRole(data);
            return Ok(result);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO login)
        {
            var result = await _usecase.Login(login);
            return Ok(result);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO register)
        {
            await _usecase.Register(register);
            return Ok("User created successfully!");
        }

        [HttpPost("Refresh")]
        public async Task<IActionResult> RefreshToken(TokenDTO token)
        {
            var result = await _usecase.RefereshToken(token);
            return Ok(result);
        }

        [HttpPost("Revoke/{username}")]
        public async Task<IActionResult> Revoke(string username)
        {
            await _usecase.Revoke(username);
            return NoContent();
        }
    }
}
