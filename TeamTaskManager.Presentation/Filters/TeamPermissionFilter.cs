using Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Services.Contracts;
using System.Security.Claims;

namespace TeamTaskManager.Presentation.Filters
{
    public class TeamPermissionFilter : IAsyncActionFilter
    {
        private readonly IAppLogger _logger;
        private readonly IServiceManager _service;

        public TeamPermissionFilter(IAppLogger logger, IServiceManager service)
        {
            _logger = logger;
            _service = service;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var user = context.HttpContext.User;
            if (user?.Identity?.IsAuthenticated ?? false)
            {
                var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    _logger.LogWarn($"{nameof(TeamPermissionFilter)} - Logged In User Id is null");
                    context.Result = new ForbidResult();
                    return;
                }

                _logger.LogInfo($"{nameof(TeamPermissionFilter)} - Logged In User Id: {userId}");
                var teamIdString = context.RouteData.Values["teamId"]?.ToString();
                if (!Guid.TryParse(teamIdString, out var teamId))
                {
                    _logger.LogWarn($"{nameof(TeamPermissionFilter)} - Invalid teamId: {teamIdString}");
                    context.Result = new ForbidResult();
                    return;
                }

                var teamCheckResult = await _service.User.UserIsATeamMember(teamId, userId!);
                if (teamCheckResult != null && teamCheckResult.Success)
                {
                    await next();
                }
                else
                {
                    _logger.LogWarn($"{nameof(TeamPermissionFilter)} - User is not a team memeber.");
                    context.Result = new ForbidResult();
                    return;
                }
            }
            else
            {
                _logger.LogWarn($"{nameof(TeamPermissionFilter)} - User not logged in.");
                context.Result = new UnauthorizedResult();
                return;
            }
        }
    }
}
