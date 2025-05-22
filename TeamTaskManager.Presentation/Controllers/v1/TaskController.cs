using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using Shared.DTO;
using TeamTaskManager.Presentation.Controllers.v1.Extensions;

namespace TeamTaskManager.Presentation.Controllers.v1
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiversion}/tasks")]
    public class TaskController : ApiControllerBase
    {
        private readonly IServiceManager _service;

        public TaskController(IServiceManager service)
        {
            this._service = service;
        }

        /// <summary>
        /// Create a task
        /// </summary>
        /// <param name="teamId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("{teamId}")]
        public async Task<IActionResult> Create([FromRoute] Guid teamId, [FromBody] TaskCreateDto request)
        {
            var result = await _service.Task.CreateAsync(teamId, request);
            if(!result.Success)
                return ProcessError(result);

            return Ok(result.GetResult<LeanTaskToReturnDto>());
        }

        /// <summary>
        /// Gets team tasks
        /// </summary>
        /// <param name="teamId"></param>
        /// <returns></returns>
        [HttpGet("{teamId}/tasks")]
        public async Task<IActionResult> Get(Guid teamId)
        {
            var result = await _service.Task.GetTeamTasksAsync(teamId);
            if (!result.Success)
                return ProcessError(result);

            return Ok(result.GetResult<List<TaskToReturnDto>>());
        }

        /// <summary>
        /// Update task status
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateStatus([FromRoute] Guid id, [FromBody] TaskStatusDto status)
        {
            var result = await _service.Task.UpdateStatus(id, status);
            if (!result.Success)
                return ProcessError(result);

            return Ok(result.GetResult<LeanTaskToReturnDto>());
        }

       /// <summary>
       /// Updates task details
       /// </summary>
       /// <param name="id"></param>
       /// <param name="request"></param>
       /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] TaskUpdateDto request)
        {
            var result = await _service.Task.Update(id, request);
            if (!result.Success)
                return ProcessError(result);

            return Ok(result.GetResult<LeanTaskToReturnDto>());
        }

        /// <summary>
        /// Deprecates a task record
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deprecate([FromRoute] Guid id)
        {
            var result = await _service.Task.Deprecate(id);
            if (!result.Success)
                return ProcessError(result);

            return Ok(result);
        }
    }
}
