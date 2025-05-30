﻿using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using Shared.DTO;
using TeamTaskManager.Presentation.Controllers.v1.Extensions;
using TeamTaskManager.Presentation.Filters;

namespace TeamTaskManager.Presentation.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiversion}/auth")]
    [ApiController]
    public class AuthenticationController : ApiControllerBase
    {
        private readonly IServiceManager _services;

        public AuthenticationController(IServiceManager services) =>
            _services = services ?? throw new ArgumentNullException(nameof(services));

        /// <summary>
        /// Registers a new user
        /// </summary>
        /// <param name="request">Role is 1 for TeamAdmin, 2 for Member</param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("register")]
        [RequestValidation]
        public async Task<IActionResult> Register([FromBody] RegistrationDto request)
        {
            var baseResponse = await _services.Authentication.Register(request);
            if (!baseResponse.Success)
            {
                return ProcessError(baseResponse);
            }

            return Ok(baseResponse.GetResult<UserToReturnDto>());
        }

        /// <summary>
        /// Login a user
        /// </summary>
        /// <param name="request"></param>
        /// <returns>The Access Token</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("login")]
        [RequestValidation]
        public async Task<IActionResult> Login([FromBody] LoginDto request)
        {
            var baseResponse = await _services.Authentication.Login(request);
            if (!baseResponse.Success)
            {
                return ProcessError(baseResponse);
            }

            return Ok(new { AccessToken = baseResponse.GetResult<string>() });
        }
    }
}
