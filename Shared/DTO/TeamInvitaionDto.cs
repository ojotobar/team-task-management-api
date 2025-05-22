namespace Shared.DTO
{
    public record TeamInvitaionDto
    {
        public List<string> UserIds { get; set; } = new List<string>();
    }
}
