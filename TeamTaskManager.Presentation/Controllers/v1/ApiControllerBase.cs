using Entities.Exceptions;
using Entities.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TeamTaskManager.Presentation.Controllers.v1
{
    /// <summary>
    /// 
    /// </summary>
    public class ApiControllerBase : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="baseResponse"></param>
        /// <returns>IActionResult</returns>
        /// <exception cref="NotImplementedException"></exception>
        [ApiExplorerSettings(IgnoreApi = true)]
        protected IActionResult ProcessError(ApiResponseBase baseResponse)
        {
            return baseResponse switch
            {
                NotFoundResponse => NotFound(new ExceptionResponse
                {
                    Message = ((NotFoundResponse)baseResponse).Message,
                    StatusCode = StatusCodes.Status404NotFound
                }),
                BadRequestResponse => BadRequest(new ExceptionResponse
                {
                    Message = ((BadRequestResponse)baseResponse).Message,
                    StatusCode = StatusCodes.Status400BadRequest
                }),
                UnAuthorizedResponse => Unauthorized(new ExceptionResponse
                {
                    Message = ((UnAuthorizedResponse)baseResponse).Message,
                    StatusCode = StatusCodes.Status401Unauthorized
                }),
                _ => throw new NotImplementedException()
            };
        }
    }
}
