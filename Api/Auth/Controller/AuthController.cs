using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Api.Auth.DTO;
using Api.Auth.Models;
using Api.Auth.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Api.Auth.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AuthController(ITokenService tokenService,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration)
        {
            _tokenService = tokenService;
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        [HttpPost("CreateRole")]
        public async Task<IActionResult> CreateRole(string roleName)
        {
            var roleExist = await _roleManager.RoleExistsAsync(roleName);
            if (!roleExist)
            {
                var roleResult = await _roleManager.CreateAsync(new IdentityRole(roleName));

                if (roleResult.Succeeded)
                {
                    return Ok($"Role {roleName} has been created!");
                }

                foreach (var error in roleResult.Errors)
                {
                    Console.WriteLine(error.Description);
                }

                return BadRequest("An error ocurred while adding role");
            }

            return BadRequest("Role already exist");
        }

        [HttpPost("AddUserToRole")]
        public async Task<IActionResult> AddUserToRole(AddUserToRoleDTO data)
        {
            var user = await _userManager.FindByEmailAsync(data.Email!);

            if (user != null)
            {
                var result = await _userManager.AddToRoleAsync(user, data.RoleName!);

                if (result.Succeeded)
                {
                    return Ok($"User {user.Email} added to the {data.RoleName} role");
                }
                
                return BadRequest($"An error ocurred while adding user to role {data.RoleName}");
            }

            return BadRequest("Unable to find user");
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO login)
        {
            var user = await _userManager.FindByNameAsync(login.UserName!);
            if (user is not null && await _userManager.CheckPasswordAsync(user, login.Password!))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName!),
                    new Claim(ClaimTypes.Email, user.Email!),
                    new Claim("id", login.UserName!),
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

                return Ok(new
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    RefreshToken = refreshToken,
                    Expiration = token.ValidTo
                });
            }

            return Unauthorized();
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO register)
        {
            var userExists = await _userManager.FindByNameAsync(register.UserName!);

            if (userExists != null)
            {
                return Conflict("User alread exists!");
            }

            ApplicationUser user = new ApplicationUser
            {
                Email = register.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = register.UserName
            };

            var result = await _userManager.CreateAsync(user, register.Password!);

            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "User creation failed");
            }

            return Ok("User created successfully!");
        }

        [HttpPost("Refresh")]
        public async Task<IActionResult> RefreshToken(TokenDTO token)
        {
            if (token is null)
            {
                return BadRequest("Invalid client request");
            }

            var accessToken = token.AcessToken ?? throw new ArgumentNullException(nameof(token));
            var refreshToken = token.RefershToken ?? throw new ArgumentNullException(nameof(token));
            var principalClaims = _tokenService.GetPrincipalFromExpiredToken(accessToken!, _configuration);

            if (principalClaims == null)
            {
                return BadRequest("Invalid access token/refresh token");
            }

            var userName = principalClaims.Identity.Name;
            var user = await _userManager.FindByNameAsync(userName!);

            if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpireTime <= DateTime.Now)
            {
                return BadRequest("Invalid access token/refresh token");
            }

            var newAccessToken = _tokenService.GenerateAccessToken(principalClaims.Claims.ToList(), _configuration);
            var newRefreshToken = _tokenService.GenerateRefreshToken();

            return Ok(new
            {
                accessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                refreshToken = newRefreshToken
            });
        }

        [HttpPost("Revoke/{username}")]
        public async Task<IActionResult> Revoke(string username)
        {
            var user = await _userManager.FindByNameAsync(username);

            if (user is null) return BadRequest("Invalid user name");

            user.RefreshToken = null;
            await _userManager.UpdateAsync(user);

            return NoContent();
        }
    }
}
