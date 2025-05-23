using Entities.Responses;
using Shared.DTO;

namespace Services.Contracts
{
    public interface ITeamService
    {
        Task<ApiResponseBase> Create(CreateTeamDto dto);
        Task<ApiResponseBase> InviteUsers(Guid teamId, TeamInvitaionDto dto);
    }
}
