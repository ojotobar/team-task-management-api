namespace Entities.Responses
{
    public sealed class NotFoundResponse : ApiResponseBase
    {
        public string Message { get; set; }

        public NotFoundResponse(string message) : base(false) =>
            Message = message;
    }
}
