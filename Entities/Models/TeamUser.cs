namespace Entities.Models
{
    public class TeamUser
    {
        public Guid TeamId { get; set; }
        public Team? Team { get; set; }

        public string UserId { get; set; } = string.Empty;
        public AppUser? User { get; set; }
    }
}
