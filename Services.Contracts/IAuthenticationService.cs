using Entities.Responses;
using Shared.DTO;

namespace Services.Contracts
{
    public interface IAuthenticationService
    {
        Task<ApiResponseBase> Login(LoginDto loginDto);
        Task<ApiResponseBase> Register(RegistrationDto registrationDto);
    }
}
