using System.Text.Json;

namespace Entities.Exceptions
{
    public class ExceptionResponse
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public override string ToString() => JsonSerializer.Serialize(this);
    }
}
