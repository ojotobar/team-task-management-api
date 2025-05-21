namespace Entities.Responses
{
    public abstract class ApiResponseBase
    {
        public bool Success { get; set; }

        public ApiResponseBase(bool success) =>
            Success = success;
    }
}
