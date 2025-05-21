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
        /// Test Endpoint to see if the controller is working
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The Id passed in.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Test()
        {
            return Ok(_services.User.GetUserId());
        }
    }
}