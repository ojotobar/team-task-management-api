namespace Entities.Responses
{
    public sealed class UnAuthorizedResponse : ApiResponseBase
    {
        public string Message { get; set; }

        public UnAuthorizedResponse(string message) : base(false) =>
            Message = message;
    }
}
