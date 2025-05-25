using Entities.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TeamTaskManager.Presentation.Filters
{
    public class RequestValidationFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var action = context.RouteData.Values["action"];
            var controller = context.RouteData.Values["controller"];
            var param = context.ActionArguments
            .SingleOrDefault(x => x.Value?.ToString()?.Contains("Dto") ?? false || (x.Value?.ToString()?.Contains("Request") ?? false)).Value;

            if (param is null)
            {
                context.Result = new BadRequestObjectResult(new ExceptionResponse()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = $"Object is null. Controller: { controller }, action: { action}"
                });
                return;
            }
            if (!context.ModelState.IsValid)
                context.Result = new UnprocessableEntityObjectResult(context.ModelState);
        }

        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}
