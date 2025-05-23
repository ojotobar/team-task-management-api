using Entities.Responses;

namespace Services.Contracts
{
    public interface IUserService
    {
        Task<ApiResponseBase> GetLoggedInUserInfo();
        Task<ApiResponseBase> GetUsers();
        Task<ApiResponseBase> UserIsATeamMember(Guid teamId, string userId);
    }
}
