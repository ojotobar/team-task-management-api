using Microsoft.AspNetCore.Mvc;

namespace TeamTaskManager.Presentation.Filters
{
    public class RequestValidationAttribute : TypeFilterAttribute
    {
        public RequestValidationAttribute() : base(typeof(RequestValidationFilter))
        { }
    }
}
