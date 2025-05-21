using Entities.Responses;
using Shared.DTO;

namespace Services.Contracts
{
    public interface ITeamService
    {
        Task<ApiResponseBase> Create(CreateTeamDto dto);
    }
}
