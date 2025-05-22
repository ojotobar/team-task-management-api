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

        public async Task<ApiResponseBase> CreateAsync(Guid teamId, TaskCreateDto dto)
        {
            var validator = new TaskValidator().Validate(dto);
            if (!validator.IsValid)
            {
                _logger.LogWarn($"{nameof(TaskService.CreateAsync)}: Validation errors: {string.Join(',', validator.Errors)}");
                return new BadRequestResponse(validator.Errors.FirstOrDefault()?.ErrorMessage ?? ResponseMessages.InvalidInput);
            }

            var loggedInUserId = _repository.User.GetLoggedInUserId();
            if (string.IsNullOrEmpty(loggedInUserId))
            {
                return new BadRequestResponse(ResponseMessages.InvalidUserCredentials);
            }

            var assignedUser = await _userManager.FindByIdAsync(dto.AssignTo);
            if (assignedUser == null)
            {
                return new NotFoundResponse(ResponseMessages.UserNotFound);
            }

            var team = await _repository.Team.FindByIdAsync(teamId, false);
            if (team == null)
            {
                _logger.LogError($"TeamId {teamId}: {ResponseMessages.TeamRecordNotFound}");
                return new NotFoundResponse(ResponseMessages.TeamRecordNotFound);
            }

            var task = dto.Map(loggedInUserId, teamId);
            await _repository.Task.AddAsync(task);
            await _repository.SaveAsync();

            return new OkResponse<LeanTaskToReturnDto>(task.Map());
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

            _repository.Task.Deprecate(task);
            await _repository.SaveAsync();

            return new OkResponse<string>(ResponseMessages.TaskDeleted);
        }
    }
}
