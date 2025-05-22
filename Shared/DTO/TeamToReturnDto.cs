namespace Shared.DTO
{
    public record TeamToReturnDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
