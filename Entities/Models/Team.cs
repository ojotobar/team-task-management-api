using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public sealed class Team : EntityBase
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();
        public ICollection<TeamUser> TeamUsers { get; set; } = new List<TeamUser>();
    }
}
