using Entities.Responses;

namespace TeamTaskManager.Presentation.Controllers.v1.Extensions
{
    public static class ApiBaseResponseExtensions
    {
        public static T GetResult<T>(this ApiResponseBase apiBaseResponse) =>
            ((OkResponse<T>)apiBaseResponse).Result;
    }
}
