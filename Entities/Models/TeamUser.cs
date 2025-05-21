using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class TeamUser : EntityBase
    {
        public Guid TeamId { get; set; }
        public Team? Team { get; set; }

        public string UserId { get; set; } = string.Empty;
        public User? User { get; set; }
    }
}