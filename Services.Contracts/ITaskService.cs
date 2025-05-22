using Entities.Responses;
using Shared.DTO;

namespace Services.Contracts
{
    public interface ITaskService
    {
        Task<ApiResponseBase> CreateAsync(Guid teamId, TaskCreateDto dto);
        Task<ApiResponseBase> Deprecate(Guid id);
        Task<ApiResponseBase> GetTeamTasksAsync(Guid teamId);
        Task<ApiResponseBase> Update(Guid id, TaskUpdateDto dto);
        Task<ApiResponseBase> UpdateStatus(Guid id, TaskStatusDto statusDto);
    }
}
