using Entities.Responses;

namespace Services.Contracts
{
    public interface IUserService
    {
        Task<ApiResponseBase> GetLoggedInUserInfo();
    }
}
