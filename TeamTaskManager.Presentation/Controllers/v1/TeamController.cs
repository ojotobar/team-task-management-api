using Asp.Versioning;
using Entities.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using Shared.DTO;
using Shared.Extrensions;

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
    }
}
