namespace Entities.Responses
{
    public sealed class OkResponse<TResult> : ApiResponseBase
    {
        public TResult Result { get; set; }

        public OkResponse(TResult result) : base(true) =>
            Result = result;
    }
}
