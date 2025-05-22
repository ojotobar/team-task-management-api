using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace TeamTaskManager.Presentation.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiversion}/users")]
    [ApiController]
    [Authorize]
    public class UserController : ApiControllerBase
    {
        private readonly IServiceManager _services;

        public UserController(IServiceManager service) => 
            this._services = service;

        /// <summary>
        /// Get loggedin user details
        /// </summary>
        /// <returns>User details</returns>
        [HttpGet("me")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetLoggedInUserDetails()
        {
            var result = await _services.User.GetLoggedInUserInfo();
            if(!result.Success)
                return ProcessError(result);

            return Ok(result);
        }

        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "TeamAdmin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _services.User.GetUsers();
            if (!result.Success)
                return ProcessError(result);

            return Ok(result);
        }
    }
}