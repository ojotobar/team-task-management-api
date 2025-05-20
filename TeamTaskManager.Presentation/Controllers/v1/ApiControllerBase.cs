using Microsoft.AspNetCore.Mvc;

namespace TeamTaskManager.Presentation.Controllers.v1
{
    public class ApiControllerBase : ControllerBase
    {
        //[ApiExplorerSettings(IgnoreApi = true)]
        //protected IActionResult ProcessError(ApiBaseResponse baseResponse)
        //{
        //    return baseResponse switch
        //    {
        //        ApiNotFoundResponse => NotFound(new ErrorDetails
        //        {
        //            Message = ((ApiNotFoundResponse)baseResponse).Message,
        //            StatusCode = StatusCodes.Status404NotFound
        //        }),
        //        ApiBadRequestResponse => BadRequest(new ErrorDetails
        //        {
        //            Message = ((ApiBadRequestResponse)baseResponse).Message,
        //            StatusCode = StatusCodes.Status400BadRequest
        //        }),
        //        ApiUnathorizedResponse => Unauthorized(new ErrorDetails
        //        {
        //            Message = ((ApiUnathorizedResponse)baseResponse).Message,
        //            StatusCode = StatusCodes.Status401Unauthorized
        //        }),
        //        _ => throw new NotImplementedException()
        //    };
        //}
    }
}
