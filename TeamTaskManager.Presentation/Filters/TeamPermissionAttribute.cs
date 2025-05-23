using Microsoft.AspNetCore.Mvc;

namespace TeamTaskManager.Presentation.Filters
{
    public class TeamPermissionAttribute : TypeFilterAttribute
    {
        public TeamPermissionAttribute() : base(typeof(TeamPermissionFilter))
        { }
    }
}
