using Asp.Versioning;
using Entities.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using Shared.DTO;
using Shared.Extrensions;
using TeamTaskManager.Presentation.Controllers.v1.Extensions;

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
            this._service = service;
        }

        /// <summary>
        /// Creates a Team.
        /// Only a User on the Team Admin role can access this endpoint
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "TeamAdmin")]
        public async Task<IActionResult> Create([FromBody] CreateTeamDto request)
        {
            var result = await _service.Team.Create(request);
            if(!result.Success)
                return ProcessError(result);

            return Ok(result);
        }

        /// <summary>
        /// Invite users to the team
        /// </summary>
        /// <param name="teamId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize(Roles = "TeamAdmin")]
        [HttpPost("{teamId}/users")]
        public async Task<IActionResult> Invite([FromRoute] Guid teamId, [FromBody] TeamInvitaionDto request)
        {
            var result = await _service.Team.InviteUsers(teamId, request);
            if (!result.Success)
                return ProcessError(result);

            return Ok(result);
        }

        /// <summary>
        /// Get a list of all teams
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "TeamAdmin")]
        public async Task<IActionResult> Get()
        {
            var result = await _service.Team.Get();
            if (!result.Success)
                return ProcessError(result);

            return Ok(result.GetResult<List<TeamToReturnDto>>());
        }

        /// <summary>
        /// Get list of team members
        /// </summary>
        /// <param name="teamId"></param>
        /// <returns></returns>
        [HttpGet("{teamId}")]
        public async Task<IActionResult> GetMembers([FromRoute] Guid teamId)
        {
            var result = await _service.Team.GetTeamMembers(teamId);
            if (!result.Success)
                return ProcessError(result);

            return Ok(result.GetResult<List<UserToReturnDto>>());
        }
    }
}
