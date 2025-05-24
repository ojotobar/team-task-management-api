namespace Shared.DTO
{
    public abstract record UserToReturnDtoBase
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }

    public record UserToReturnDto : UserToReturnDtoBase
    {
    }

    public record UserWithTeamsToReturnDto : UserToReturnDtoBase
    {
        public List<TeamToReturnDto> Teams { get; set; } = new List<TeamToReturnDto>();
    }
}
