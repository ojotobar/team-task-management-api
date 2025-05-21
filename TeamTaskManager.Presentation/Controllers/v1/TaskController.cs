using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

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

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(new object[0]);
        }
    }
}
