using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public sealed class TaskItem : EntityBase
    {
        [Required]
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime? DueDate { get; set; }
        [Required]
        public TaskStatus Status { get; set; }

        public string CreatedByUserId { get; set; } = string.Empty;
        //Navigation Property
        public AppUser? CreatedByUser { get; set; }

        public string AssignedToUserId { get; set; } = string.Empty;
        //Navigation Property
        public AppUser? AssignedToUser { get; set; }

        public Guid TeamId { get; set; }
        //Navigation Property
        public Team? Team { get; set; }
    }
}
