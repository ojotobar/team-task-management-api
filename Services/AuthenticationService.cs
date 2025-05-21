using Contracts;
using Entities.Models;
using Entities.Responses;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Services.Contracts;
using Shared;
using Shared.DTO;
using Shared.Extrensions;

namespace Services
{
    public sealed class AuthenticationService : IAuthenticationService
    {
        private readonly IAppLogger _logger;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;

        public AuthenticationService(IAppLogger logger, UserManager<User> userManager, 
            IConfiguration configuration)
        {
            _logger = logger;
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<ApiResponseBase> Register(RegistrationDto registrationDto)
        {
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
            return new OkResponse<string>("");
        }
    }
}
