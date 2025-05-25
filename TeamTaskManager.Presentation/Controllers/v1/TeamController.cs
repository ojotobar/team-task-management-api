using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using Shared.DTO;
using TeamTaskManager.Presentation.Filters;
using TeamTaskManager.Presentation.Controllers.v1.Extensions;
using Microsoft.AspNetCore.Http;

namespace TeamTaskManager.Presentation.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiversion}/teams")]
    [Authorize]
    [ApiController]
    public class TeamController : ApiControllerBase
    {
        private readonly IServiceManager _service;

        public TeamController(IServiceManager service)
        {
            _service = service;
        }

        /// <summary>
        /// Creates a Team.
        /// Only a User on the Team Admin role can access this endpoint
        /// This action will add the team creator as a default member of the team.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "TeamAdmin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] CreateTeamDto request)
        {
            var result = await _service.Team.Create(request);
            if(!result.Success)
                return ProcessError(result);

            return Ok(result.GetResult<TeamToReturnDto>());
        }

        /// <summary>
        /// Invite users to the team.
        /// Only a team member can invite a user to the team
        /// </summary>
        /// <param name="teamId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("{teamId}/users")]
        [TeamPermission]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Invite([FromRoute] Guid teamId, [FromBody] TeamInvitaionDto request)
        {
            var result = await _service.Team.InviteUsers(teamId, request);
            if (!result.Success)
                return ProcessError(result);

            return Ok(result);
        }

        /// <summary>
        /// Gets team tasks
        /// </summary>
        /// <param name="teamId"></param>
        /// <returns></returns>
        [HttpGet("{teamId}/tasks")]
        [TeamPermission]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTeamTasks([FromRoute] Guid teamId)
        {
            var result = await _service.Task.GetTeamTasksAsync(teamId);
            if (!result.Success)
                return ProcessError(result);

            return Ok(result.GetResult<List<TaskToReturnDto>>());
        }



        /// <summary>
        /// Create one or more task
        /// </summary>
        /// <param name="teamId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("{teamId}/tasks")]
        [TeamPermission]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateTasks([FromRoute] Guid teamId, [FromBody] List<TaskCreateDto> request)
        {
            var result = await _service.Task.CreateManyAsync(teamId, request);
            if (!result.Success)
                return ProcessError(result);

            return Ok(result.GetResult<List<LeanTaskToReturnDto>>());
        }
    }
}
