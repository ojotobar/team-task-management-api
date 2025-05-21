using Contracts;
using Entities.Models;
using Entities.Responses;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Services.Contracts;
using Shared;
using Shared.DTO;
using Shared.Extrensions;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Services
{
    public sealed class AuthenticationService : IAuthenticationService
    {
        private readonly IAppLogger _logger;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private User? _user;

        public AuthenticationService(IAppLogger logger, UserManager<User> userManager, 
            IConfiguration configuration)
        {
            _logger = logger;
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<ApiResponseBase> Register(RegistrationDto registrationDto)
        {
            if (registrationDto.Password.Equals(registrationDto.ConfirmPassword))
            {
                return new BadRequestResponse(ResponseMessages.PasswordMustMatch);
            }

            var user = registrationDto.Map();

            var result = await _userManager.CreateAsync(user, registrationDto.Password);
            if (result.Succeeded)
            {
                var roleResult = await _userManager.AddToRoleAsync(user, registrationDto.Role.GetDescription());
                if (roleResult.Succeeded)
                {
                    return new OkResponse<string>(ResponseMessages.RegistrationSuccessful);
                }
                else
                {
                    await _userManager.DeleteAsync(user);
                    return new BadRequestResponse(roleResult.Errors.FirstOrDefault()?.Description ?? ResponseMessages.GenericErrorMessage);
                }
            }

            return new BadRequestResponse(result.Errors.FirstOrDefault()?.Description ?? ResponseMessages.GenericErrorMessage);
        }

        public async Task<ApiResponseBase> Login(LoginDto loginDto)
        {
            var validated = await IsUserValidated(loginDto);
            if (!validated)
            {
                return new UnAuthorizedResponse(ResponseMessages.WrongEmailOrPassword);
            }

            return new OkResponse<string>(await CreateAccessToken());
        }

        private async Task<bool> IsUserValidated(LoginDto loginDto)
        {
            _user = await _userManager.FindByEmailAsync(loginDto.Email);
            if(_user == null) return false;

            var isPasswordvalidated = await _userManager.CheckPasswordAsync(_user, loginDto.Password);
            if (!isPasswordvalidated)
            {
                _logger.LogWarn($"Wrong email or password");
            }

            return isPasswordvalidated;
        }

        private async Task<string> CreateAccessToken()
        {
            var tokenOptions = await GetTokenOptions();
            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }

        private SigningCredentials GetSigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes(_configuration.GetSection("JwtSettings")["Secret"]);
            var secret = new SymmetricSecurityKey(key);

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private async Task<List<Claim>> GetClaims()
        {
            var claims = new List<Claim>();

            if(_user != null)
            {
                claims.AddRange(new List<Claim>
                {
                    new Claim(ClaimTypes.Name, _user.UserName),
                    new Claim(ClaimTypes.NameIdentifier, _user.Id)
                });

                var roles = await _userManager.GetRolesAsync(_user);
                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }
            }

            return claims;
        }

        private async Task<JwtSecurityToken> GetTokenOptions()
        {
            var jwtSection = _configuration.GetSection("JwtSettings");

            return new JwtSecurityToken(issuer: jwtSection["Issuer"], audience: jwtSection["Audience"], claims: await GetClaims(),
                expires: DateTime.Now.AddHours(Convert.ToDouble(jwtSection["Expires"])), signingCredentials: GetSigningCredentials());
        }
    }
}
