using Contracts;
using Entities.Models;
using Entities.Responses;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Services.Contracts;
using Services.Validations;
using Shared;
using Shared.DTO;
using Shared.Extrensions;

namespace Services
{
    public sealed class TaskService : ITaskService
    {
        private readonly IRepositoryManager _repository;
        private readonly IAppLogger _logger;
        private readonly UserManager<User> _userManager;

        public TaskService(IRepositoryManager repository, IAppLogger logger, UserManager<User> userManager)
        {
            _logger = logger;
            _userManager = userManager;
            _repository = repository;
        }

        public async Task<ApiResponseBase> CreateManyAsync(Guid teamId, List<TaskCreateDto> dtos)
        {
            var validationResult = await ValidateInput(dtos);
            if (!validationResult.Success)
            {
                return validationResult;
            }
            var loggedInUserId = _repository.User.GetLoggedInUserId();
            if (string.IsNullOrEmpty(loggedInUserId))
            {
                return new BadRequestResponse(ResponseMessages.InvalidUserCredentials);
            }

            var team = await _repository.Team.FindByIdAsync(teamId, false);
            if (team == null)
            {
                _logger.LogError($"TeamId {teamId}: {ResponseMessages.TeamRecordNotFound}");
                return new NotFoundResponse(ResponseMessages.TeamRecordNotFound);
            }

            var tasks = dtos.Map(loggedInUserId, teamId);
            await _repository.Task.AddRangeAsync(tasks);
            await _repository.SaveAsync();

            return new OkResponse<List<LeanTaskToReturnDto>>(tasks.MapTo());
        }

        public async Task<ApiResponseBase> GetTeamTasksAsync(Guid teamId)
        {
            var teamTasks = await _repository.Task.Query(teamId, true)
                .OrderByDescending(t => t.CreatedAt)
                .ToListAsync();

            return new OkResponse<List<TaskToReturnDto>>(teamTasks.Map());
        }

        public async Task<ApiResponseBase> UpdateStatus(Guid id, TaskStatusDto statusDto)
        {
            var validator = new TaskStatusValidator().Validate(statusDto);
            if (!validator.IsValid)
            {
                _logger.LogWarn($"{nameof(TaskService.UpdateStatus)}: Validation errors: {string.Join(',', validator.Errors)}");
                return new BadRequestResponse(validator.Errors.FirstOrDefault()?.ErrorMessage ?? ResponseMessages.InvalidInput);
            }

            var task = await _repository.Task.FindByIdAsync(id, true);
            if(task == null)
            {
                _logger.LogError($"TaskId {id}: {ResponseMessages.TaskRecordNotFound}");
                return new NotFoundResponse(ResponseMessages.TeamRecordNotFound);
            }

            var isUserPermitted = await UserBelongsToTeam(task.TeamId);
            if (!isUserPermitted)
            {
                _logger.LogError($"TaskId {id}: {ResponseMessages.PermissionDenied}");
                return new ForbidResponse(ResponseMessages.PermissionDenied);
            }

            if (statusDto.Status.Equals(task.Status))
            {
                return new BadRequestResponse(string.Format(ResponseMessages.AlreadyInStatus, statusDto.Status.GetDescription()));
            }

            task.Status = statusDto.Status;
            _repository.Task.Edit(task);
            await _repository.SaveAsync();

            return new OkResponse<LeanTaskToReturnDto>(task.Map());
        }

        public async Task<ApiResponseBase> Update(Guid id, TaskUpdateDto dto)
        {
            var validator = new TaskValidator().Validate(dto);
            if (!validator.IsValid)
            {
                _logger.LogWarn($"{nameof(TaskService.Update)}: Validation errors: {string.Join(',', validator.Errors)}");
                return new BadRequestResponse(validator.Errors.FirstOrDefault()?.ErrorMessage ?? ResponseMessages.InvalidInput);
            }

            var task = await _repository.Task.FindByIdAsync(id, true);
            if (task == null)
            {
                _logger.LogError($"TaskId {id}: {ResponseMessages.TaskRecordNotFound}");
                return new NotFoundResponse(ResponseMessages.TaskRecordNotFound);
            }

            var isUserPermitted = await UserBelongsToTeam(task.TeamId);
            if (!isUserPermitted)
            {
                _logger.LogError($"TaskId {id}: {ResponseMessages.PermissionDenied}");
                return new ForbidResponse(ResponseMessages.PermissionDenied);
            }

            var assignedUser = await _userManager.FindByIdAsync(dto.AssignTo);
            if (assignedUser == null)
            {
                return new NotFoundResponse(ResponseMessages.UserNotFound);
            }

            task = task.Map(dto);
            _repository.Task.Edit(task);
            await _repository.SaveAsync();

            return new OkResponse<LeanTaskToReturnDto>(task.Map());
        }

        public async Task<ApiResponseBase> Deprecate(Guid id)
        {
            var task = await _repository.Task.FindByIdAsync(id, true);
            if (task == null)
            {
                _logger.LogError($"TaskId {id}: {ResponseMessages.TaskRecordNotFound}");
                return new NotFoundResponse(ResponseMessages.TaskRecordNotFound);
            }

            var isUserPermitted = await UserBelongsToTeam(task.TeamId);
            if (!isUserPermitted)
            {
                _logger.LogError($"TaskId {id}: {ResponseMessages.PermissionDenied}");
                return new ForbidResponse(ResponseMessages.PermissionDenied);
            }

            _repository.Task.Deprecate(task);
            await _repository.SaveAsync();

            return new OkResponse<string>(ResponseMessages.TaskDeleted);
        }

        private async Task<bool> UserBelongsToTeam(Guid teamId)
        {
            var userId = _repository.User.GetLoggedInUserId();
            var user = await _repository.TeamUser
                .FindBy(tu => tu.TeamId.Equals(teamId) && tu.UserId.Equals(userId))
                .FirstOrDefaultAsync();

            return user != null;
        }

        private async Task<ApiResponseBase> ValidateInput(List<TaskCreateDto> dtos)
        {
            var userIds = dtos.Select(d => d.AssignTo).ToList();
            var assignedUsers = await _userManager.Users.Where(u => userIds.Contains(u.Id))
                .ToListAsync();

            foreach (var dto in dtos)
            {
                var validator = new TaskValidator().Validate(dto);
                if (!validator.IsValid)
                {
                    _logger.LogWarn($"{nameof(TaskService.CreateManyAsync)}: Validation errors: {string.Join(',', validator.Errors)}");
                    return new BadRequestResponse(validator.Errors.FirstOrDefault()?.ErrorMessage ?? ResponseMessages.InvalidInput);
                }

                var assignedUser = assignedUsers.FirstOrDefault(u => u.Id.Equals(dto.AssignTo));
                if (assignedUser == null)
                {
                    return new NotFoundResponse(ResponseMessages.UserNotFound);
                }

            }

            return new OkResponse<string>("Validated OK");
        }
    }
}
