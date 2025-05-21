namespace Entities.Responses
{
    public sealed class BadRequestResponse : ApiResponseBase
    {
        public string Message { get; set; }

        public BadRequestResponse(string message) : base(false) =>
            Message = message;
    }
}
