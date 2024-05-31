using FullCoffee.Core.DTOs;
using FullCoffee.Core.Models;
using FullCoffee.Service.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FullCoffee.API.Controllers
{
    public class UserController : CustomBaseController
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly IConfiguration _configuration;

        public UserController(
            UserManager<User> userManager,
            RoleManager<IdentityRole<int>> roleManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim("Id", user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var token = GetToken(authClaims);

                var response = new LoginResponseDto
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    Expiration = token.ValidTo
                };
                return CreateActionResult(CustomResponseDto<LoginResponseDto>.Success(200, response));
            }
            return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(401, "User Not Found."));
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            var userExists = await _userManager.FindByEmailAsync(model.Email);
            if (userExists != null)
                return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(500, "User already exists!"));

            User user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(500, "User creation failed! Please check user details and try again."));

            return CreateActionResult(CustomResponseDto<LoginResponseDto>.Success(200));
        }

        [HttpPost]
        [Route("register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterDto model)
        {
            try
            {
                var userExists = await _userManager.FindByEmailAsync(model.Email);
                if (userExists != null)
                    return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(500, "User already exists!"));


                User user = new()
                {
                    Email = model.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = model.Username,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (!result.Succeeded)
                    return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(500, "User creation failed! Please check user details and try again."));

                if (!await _roleManager.RoleExistsAsync(UserRoleHelper.Admin))
                    await _roleManager.CreateAsync(new IdentityRole<int>(UserRoleHelper.Admin));
                if (!await _roleManager.RoleExistsAsync(UserRoleHelper.User))
                    await _roleManager.CreateAsync(new IdentityRole<int>(UserRoleHelper.User));

                if (await _roleManager.RoleExistsAsync(UserRoleHelper.Admin))
                {
                    await _userManager.AddToRoleAsync(user, UserRoleHelper.Admin);
                }
                if (await _roleManager.RoleExistsAsync(UserRoleHelper.User))
                {
                    await _userManager.AddToRoleAsync(user, UserRoleHelper.User);
                }
                return CreateActionResult(CustomResponseDto<LoginResponseDto>.Success(200));
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
    }

}
