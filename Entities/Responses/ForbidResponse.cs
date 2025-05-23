namespace Entities.Responses
{
    public class ForbidResponse : ApiResponseBase
    {
        public string Message { get; set; }

        public ForbidResponse(string message) : base(false) =>
            Message = message;
    }
}
