using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using Shared.DTO;
using TeamTaskManager.Presentation.Controllers.v1.Extensions;
using TeamTaskManager.Presentation.Filters;

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
        /// Updates task details
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("{taskId}")]
        [RequestValidation]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromRoute] Guid taskId, [FromBody] TaskUpdateDto request)
        {
            var result = await _service.Task.Update(taskId, request);
            if (!result.Success)
                return ProcessError(result);

            return Ok(result.GetResult<LeanTaskToReturnDto>());
        }

        /// <summary>
        /// Deprecates a task record
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        [HttpDelete("{taskId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Deprecate([FromRoute] Guid taskId)
        {
            var result = await _service.Task.Deprecate(taskId);
            if (!result.Success)
                return ProcessError(result);

            return Ok(result);
        }

        /// <summary>
        /// Update task status. 0 - Pending, 1 - In Progress, 2 - Completed
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPatch("{taskId}")]
        [RequestValidation]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateStatus([FromRoute] Guid taskId, [FromBody] TaskStatusDto request)
        {
            var result = await _service.Task.UpdateStatus(taskId, request);
            if (!result.Success)
                return ProcessError(result);

            return Ok(result.GetResult<LeanTaskToReturnDto>());
        }
    }
}
